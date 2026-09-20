using System.Collections.Generic;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Core.Services.Contract;

public interface IChunker
{
    IEnumerable<Chunk> ChunkDocument(DocumentRecord document, IEnumerable<ExtractedSection> sections);
}
