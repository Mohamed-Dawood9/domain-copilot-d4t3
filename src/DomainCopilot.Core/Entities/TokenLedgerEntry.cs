using System;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Core.Entities;

public class TokenLedgerEntry : BaseEntity
{
    public Guid BudgetId { get; private set; }
    public int TokensUsed { get; private set; }
    public Money Cost { get; private set; }
    public string ModelUsed { get; private set; }
    public DateTime Timestamp { get; private set; }

    public TokenLedgerEntry(Guid budgetId, int tokensUsed, Money cost, string modelUsed)
    {
        BudgetId = budgetId;
        TokensUsed = tokensUsed;
        Cost = cost;
        ModelUsed = modelUsed;
        Timestamp = DateTime.UtcNow;
    }
}
