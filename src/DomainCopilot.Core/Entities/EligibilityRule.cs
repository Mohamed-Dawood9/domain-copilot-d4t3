using System;

namespace DomainCopilot.Core.Entities;

public class EligibilityRule : BaseEntity
{
    public Guid ServiceId { get; private set; }
    public string Description { get; private set; }
    public string SourceDocumentId { get; private set; }

    public EligibilityRule(Guid serviceId, string description, string sourceDocumentId)
    {
        ServiceId = serviceId;
        Description = description;
        SourceDocumentId = sourceDocumentId;
    }
}
