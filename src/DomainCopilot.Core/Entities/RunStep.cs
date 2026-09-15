using System;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Core.Entities;

public class RunStep : BaseEntity
{
    public Guid AgentRunId { get; private set; }
    public string AgentName { get; private set; }
    public string ModelUsed { get; private set; }
    public int TokensUsed { get; private set; }
    public Money Cost { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string ActionTaken { get; private set; }

    public RunStep(Guid agentRunId, string agentName, string modelUsed, int tokensUsed, Money cost, string actionTaken)
    {
        AgentRunId = agentRunId;
        AgentName = agentName;
        ModelUsed = modelUsed;
        TokensUsed = tokensUsed;
        Cost = cost;
        ActionTaken = actionTaken;
        Timestamp = DateTime.UtcNow;
    }
}
