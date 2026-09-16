using System;
using DomainCopilot.Core.Enums;

namespace DomainCopilot.Core.Entities;

public class CitizenCase : BaseEntity
{
    public string Description { get; private set; }
    public Guid? MatchedServiceId { get; private set; }
    public CaseStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public CitizenCase(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty.", nameof(description));

        Description = description;
        Status = CaseStatus.Open;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(CaseStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetMatchedService(Guid serviceId)
    {
        MatchedServiceId = serviceId;
        UpdatedAt = DateTime.UtcNow;
    }
}
