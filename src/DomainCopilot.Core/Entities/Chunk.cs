using System;

namespace DomainCopilot.Core.Entities;

public class Chunk : BaseEntity
{
    public Guid DocumentRecordId { get; private set; }
    public string Content { get; private set; }
    public int ChunkIndex { get; private set; }
    public string EmbeddingId { get; private set; }
    public int TokenCount { get; private set; }

    // Navigation property for EF Core
    public DocumentRecord DocumentRecord { get; private set; }

    public Chunk(Guid documentRecordId, string content, int chunkIndex, string embeddingId, int tokenCount)
    {
        DocumentRecordId = documentRecordId;
        Content = content;
        ChunkIndex = chunkIndex;
        EmbeddingId = embeddingId;
        TokenCount = tokenCount;
    }
}
