using System;
using System.Collections.Generic;
using System.Linq;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Repository.Ingestion.Chunkers;

public class SlidingWindowChunker : IChunker
{
    private readonly int _windowSize;
    private readonly int _overlap;

    public SlidingWindowChunker(int windowSize = 500, int overlap = 75) // ~15% overlap
    {
        _windowSize = windowSize;
        _overlap = overlap;
    }

    public IEnumerable<Chunk> ChunkDocument(DocumentRecord document, IEnumerable<ExtractedSection> sections)
    {
        var chunks = new List<Chunk>();
        int chunkIndex = 0;

        foreach (var section in sections)
        {
            var words = section.Content.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int currentIdx = 0;

            while (currentIdx < words.Length)
            {
                var chunkWords = words.Skip(currentIdx).Take(_windowSize).ToArray();
                var content = string.Join(" ", chunkWords);

                var contextHeader = $"[Document: {document.Source} | Version: {document.Version} | Effective: {document.EffectiveDate:yyyy-MM-dd} | Section: {section.SectionTitle} | Page: {section.PageNumber}]\n";
                var fullChunkContent = contextHeader + content;

                chunks.Add(new Chunk(document.Id, fullChunkContent, chunkIndex++, string.Empty, fullChunkContent.Length / 4));

                if (currentIdx + _windowSize >= words.Length)
                {
                    break;
                }

                currentIdx += _windowSize - _overlap;
            }
        }

        return chunks;
    }
}
