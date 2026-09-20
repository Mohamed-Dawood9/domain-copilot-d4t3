using System;
using DomainCopilot.Core.Enums;

namespace DomainCopilot.Core.Entities;

public class DocumentRecord : BaseEntity
{
    public string Source { get; private set; }
    public string Section { get; private set; }
    public string Version { get; private set; }
    public int Page { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public string FileFormat { get; private set; }
    public string Content { get; private set; }
    public string ContentHash { get; private set; }
    public IngestionStatus Status { get; private set; }
    public string? FailureReason { get; private set; }

    public DocumentRecord(string source, string section, string version, int page, DateTime effectiveDate, string fileFormat, string content, string contentHash)
    {
        Source = source;
        Section = section;
        Version = version;
        Page = page;
        EffectiveDate = effectiveDate;
        FileFormat = fileFormat;
        Content = content;
        ContentHash = contentHash;
        Status = IngestionStatus.Pending;
    }

    public void UpdateStatus(IngestionStatus status, string failureReason = null)
    {
        Status = status;
        FailureReason = failureReason;
    }
}
