using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Repository.Data;
using DomainCopilot.Repository.Repositories;
using DomainCopilot.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DomainCopilot.Repository.IntegrationTests.Ingestion;

public class IngestionServiceTests
{
    [Fact]
    public async Task ProcessFileAsync_SuccessFlow_ExtractsChunksEmbedsAndSaves()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new AppDbContext(options);

        // Use real EF Core repositories against InMemory DB
        var documentRepo = new DocumentRepository(dbContext);
        var chunkRepo = new GenericRepository<Chunk>(dbContext);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(() => dbContext.SaveChangesAsync());

        var mockEmbeddingProvider = new Mock<IEmbeddingProvider>();
        mockEmbeddingProvider.Setup(x => x.EmbedAsync(It.IsAny<string>()))
                             .ReturnsAsync(new float[] { 0.1f, 0.2f, 0.3f });

        var mockVectorStore = new Mock<IVectorStore>();
        mockVectorStore.Setup(x => x.UpsertAsync(It.IsAny<Chunk>(), It.IsAny<float[]>()))
                       .Returns(Task.CompletedTask);

        var mockLogger = new Mock<ILogger<IngestionService>>();

        var mockExtractor = new Mock<IDocumentExtractor>();
        mockExtractor.Setup(x => x.ExtractAsync(It.IsAny<Stream>()))
            .ReturnsAsync(new[] { new DomainCopilot.Core.ValueObjects.ExtractedSection("Title", "Content", 1) });

        var mockExtractorFactory = new Mock<IExtractorFactory>();
        mockExtractorFactory.Setup(x => x.GetExtractor(It.IsAny<string>())).Returns(mockExtractor.Object);

        var mockTextCleaner = new Mock<ITextCleaner>();
        mockTextCleaner.Setup(x => x.Clean(It.IsAny<System.Collections.Generic.IEnumerable<DomainCopilot.Core.ValueObjects.ExtractedSection>>()))
            .Returns(new[] { new DomainCopilot.Core.ValueObjects.ExtractedSection("Title", "Content", 1) });

        var mockChunker = new Mock<IChunker>();
        mockChunker.Setup(x => x.ChunkDocument(It.IsAny<DocumentRecord>(), It.IsAny<System.Collections.Generic.IEnumerable<DomainCopilot.Core.ValueObjects.ExtractedSection>>()))
            .Returns(new[] { new Chunk(Guid.NewGuid(), "Content", 0, Guid.NewGuid().ToString(), 1) });

        var chunkers = new[] { new DomainCopilot.Repository.Ingestion.Chunkers.StructuralChunker() };

        var ingestionService = new IngestionService(
            documentRepo,
            chunkRepo,
            mockUnitOfWork.Object,
            mockEmbeddingProvider.Object,
            mockVectorStore.Object,
            mockLogger.Object,
            mockExtractorFactory.Object,
            mockTextCleaner.Object,
            chunkers
        );

        // Create a temporary test file
        var testFilePath = Path.Combine(Path.GetTempPath(), "test_policy.txt");
        await File.WriteAllTextAsync(testFilePath, "Section 1.1\nThis is a test policy document for the ingestion service.");

        try
        {
            // Act
            await ingestionService.ProcessFileAsync(testFilePath, "test_policy", "General", "1.0", DateTime.UtcNow.Date);

            // Assert
            var documents = await documentRepo.ListAllAsync();
            var doc = documents.FirstOrDefault();

            Assert.NotNull(doc);
            Assert.Equal("test_policy", doc.Source);
            Assert.Equal(DomainCopilot.Core.Enums.IngestionStatus.Completed, doc.Status);

            var chunks = await chunkRepo.ListAllAsync();
            Assert.NotEmpty(chunks);

            // Verify vector store was called for each chunk
            mockVectorStore.Verify(x => x.UpsertAsync(It.IsAny<Chunk>(), It.IsAny<float[]>()), Times.Exactly(chunks.Count()));

            // Verify embedding provider was called
            mockEmbeddingProvider.Verify(x => x.EmbedAsync(It.IsAny<string>()), Times.Exactly(chunks.Count()));
        }
        finally
        {
            // Cleanup
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }
    }
}
