using System;
using DomainCopilot.Core.Enums;

namespace DomainCopilot.Core.Entities;

public class ApprovalDecision : BaseEntity
{
    public Guid CitizenCaseId { get; private set; }
    public string OfficerId { get; private set; }
    public ApprovalStatus Status { get; private set; }
    public string Feedback { get; private set; }
    public DateTime DecidedAt { get; private set; }

    public ApprovalDecision(Guid citizenCaseId, string officerId, ApprovalStatus status, string feedback)
    {
        CitizenCaseId = citizenCaseId;
        OfficerId = officerId;
        Status = status;
        Feedback = feedback;
        DecidedAt = DateTime.UtcNow;
    }
}
