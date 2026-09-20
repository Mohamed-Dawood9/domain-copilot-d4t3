using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Enums;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Core.Services.Contract;
using Microsoft.Extensions.Logging;

namespace DomainCopilot.Service.Services;

public class IngestionService : IIngestionService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IGenericRepository<Chunk> _chunkRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmbeddingProvider _embeddingProvider;
    private readonly IVectorStore _vectorStore;
    private readonly ILogger<IngestionService> _logger;

    private readonly IExtractorFactory _extractorFactory;
    private readonly ITextCleaner _textCleaner;
    private readonly IEnumerable<IChunker> _chunkers;

    public IngestionService(
        IDocumentRepository documentRepository,
        IGenericRepository<Chunk> chunkRepository,
        IUnitOfWork unitOfWork,
        IEmbeddingProvider embeddingProvider,
        IVectorStore vectorStore,
        ILogger<IngestionService> logger,
        IExtractorFactory extractorFactory,
        ITextCleaner textCleaner,
        IEnumerable<IChunker> chunkers)
    {
        _documentRepository = documentRepository;
        _chunkRepository = chunkRepository;
        _unitOfWork = unitOfWork;
        _embeddingProvider = embeddingProvider;
        _vectorStore = vectorStore;
        _logger = logger;
        _extractorFactory = extractorFactory;
        _textCleaner = textCleaner;
        _chunkers = chunkers;
    }

    public async Task ProcessFileAsync(string filePath, string sourceName, string section, string version, DateTime effectiveDate)
    {
        if (!File.Exists(filePath))
        {
            _logger.LogError($"File not found: {filePath}");
            return;
        }

        string fileHash = ComputeFileHash(filePath);

        // Check idempotency
        var existingDocs = await _documentRepository.ListAllAsync();
        var existingDoc = existingDocs.FirstOrDefault(d => d.Source == sourceName && d.Version == version);

        if (existingDoc != null)
        {
            if (existingDoc.ContentHash == fileHash && existingDoc.Status == IngestionStatus.Completed)
            {
                _logger.LogInformation($"Skipping {sourceName} version {version} - already ingested and unchanged.");
                return; // idempotent skip
            }
        }

        var documentRecord = new DocumentRecord(
            sourceName,
            section,
            version,
            1,
            effectiveDate,
            Path.GetExtension(filePath),
            string.Empty,
            fileHash);

        await _documentRepository.AddAsync(documentRecord);
        await _unitOfWork.CompleteAsync(); // Save DocumentRecord first to get ID

        try
        {
            documentRecord.UpdateStatus(IngestionStatus.Extracting);
            await _unitOfWork.CompleteAsync();

            var extractor = _extractorFactory.GetExtractor(Path.GetExtension(filePath));

            IEnumerable<DomainCopilot.Core.ValueObjects.ExtractedSection> extractedSections;
            using (var stream = File.OpenRead(filePath))
            {
                extractedSections = await extractor.ExtractAsync(stream);
            }

            documentRecord.UpdateStatus(IngestionStatus.Chunking);
            await _unitOfWork.CompleteAsync();

            var chunkerType = sourceName.Contains("faq", StringComparison.OrdinalIgnoreCase)
                ? "SlidingWindowChunker"
                : "StructuralChunker";

            var chunker = _chunkers.FirstOrDefault(c => c.GetType().Name == chunkerType);
            if (chunker == null) throw new Exception($"Chunker {chunkerType} not found.");

            var cleanedSections = _textCleaner.Clean(extractedSections);
            var chunks = chunker.ChunkDocument(documentRecord, cleanedSections).ToList();

            documentRecord.UpdateStatus(IngestionStatus.Embedding);
            await _unitOfWork.CompleteAsync();

            foreach (var chunk in chunks)
            {
                var vector = await _embeddingProvider.EmbedAsync(chunk.Content);
                var chunkId = Guid.NewGuid();
                var newChunk = new Chunk(documentRecord.Id, chunk.Content, chunk.ChunkIndex, chunkId.ToString(), chunk.Content.Length / 4);

                await _chunkRepository.AddAsync(newChunk);
                await _vectorStore.UpsertAsync(newChunk, vector);
            }

            documentRecord.UpdateStatus(IngestionStatus.Completed);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation($"Successfully ingested {sourceName}.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ingestion failed for {sourceName}");
            documentRecord.UpdateStatus(IngestionStatus.Failed, ex.Message);
            await _unitOfWork.CompleteAsync();
        }
    }

    private string ComputeFileHash(string filePath)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
