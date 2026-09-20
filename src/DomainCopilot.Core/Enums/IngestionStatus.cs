namespace DomainCopilot.Core.Enums;

public enum IngestionStatus
{
    Pending,
    Extracting,
    Chunking,
    Embedding,
    Indexing,
    Completed,
    Failed
}
