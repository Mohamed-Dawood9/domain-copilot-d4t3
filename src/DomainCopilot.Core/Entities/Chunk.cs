using System;

namespace DomainCopilot.Core.Entities;

public class Chunk : BaseEntity
{
    public Guid DocumentRecordId { get; private set; }
    public string Text { get; private set; }
    public float[] Embedding { get; private set; }
    public int TokenCount { get; private set; }

    public Chunk(Guid documentRecordId, string text, float[] embedding, int tokenCount)
    {
        DocumentRecordId = documentRecordId;
        Text = text;
        Embedding = embedding;
        TokenCount = tokenCount;
    }
}
