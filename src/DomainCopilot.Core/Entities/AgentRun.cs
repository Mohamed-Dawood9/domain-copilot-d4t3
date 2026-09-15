using System;

namespace DomainCopilot.Core.Entities;

public class AgentRun : BaseEntity
{
    public Guid CitizenCaseId { get; private set; }
    public string Goal { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public bool Success { get; private set; }

    public AgentRun(Guid citizenCaseId, string goal)
    {
        CitizenCaseId = citizenCaseId;
        Goal = goal;
        StartedAt = DateTime.UtcNow;
        Success = false;
    }

    public void Complete(bool success)
    {
        CompletedAt = DateTime.UtcNow;
        Success = success;
    }
}
