using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Repository.Ingestion.Chunkers;

public class StructuralChunker : IChunker
{
    public IEnumerable<Chunk> ChunkDocument(DocumentRecord document, IEnumerable<ExtractedSection> sections)
    {
        var chunks = new List<Chunk>();
        int chunkIndex = 0;

        foreach (var section in sections)
        {
            // If it's a fee schedule, handle specially
            if (document.Source.Contains("fee", StringComparison.OrdinalIgnoreCase))
            {
                var lines = section.Content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Contains("$"))
                    {
                        // Ensure effective date is injected if not already in the line
                        var effectiveDateStr = $"[Effective Date: {document.EffectiveDate:yyyy-MM-dd}]";
                        var content = $"{effectiveDateStr} {line.Trim()}";

                        // Pass string.Empty for EmbeddingId for now, it gets updated later when embedding
                        chunks.Add(new Chunk(document.Id, content, chunkIndex++, string.Empty, content.Length / 4));
                    }
                }
            }
            else
            {
                // Structural splitting based on Markdown headers or numbered clauses (e.g. "Section 1.1", "1.", "#")
                var splitPattern = @"(?=^#+\s|^\*\*(?:Section|Chapter|Article|Part)\b|^\d+\.)";
                var parts = Regex.Split(section.Content, splitPattern, RegexOptions.Multiline);

                foreach (var part in parts)
                {
                    var trimmed = part.Trim();
                    if (trimmed.Length > 0)
                    {
                        var contextHeader = $"[Document: {document.Source} | Version: {document.Version} | Effective: {document.EffectiveDate:yyyy-MM-dd} | Section: {section.SectionTitle} | Page: {section.PageNumber}]\n";
                        var fullChunkContent = contextHeader + trimmed;

                        chunks.Add(new Chunk(document.Id, fullChunkContent, chunkIndex++, string.Empty, fullChunkContent.Length / 4));
                    }
                }
            }
        }

        return chunks;
    }
}
