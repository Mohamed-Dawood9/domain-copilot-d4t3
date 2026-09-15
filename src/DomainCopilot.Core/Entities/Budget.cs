using System;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Core.Entities;

public class Budget : BaseEntity
{
    public string UserId { get; private set; }
    public Money Allocated { get; private set; }
    public Money Spent { get; private set; }
    public int TotalTokensUsed { get; private set; }

    public Budget(string userId, Money allocated)
    {
        UserId = userId;
        Allocated = allocated;
        Spent = new Money(0, allocated.Currency);
        TotalTokensUsed = 0;
    }

    public void AddSpend(Money cost, int tokens)
    {
        Spent = new Money(Spent.Amount + cost.Amount, Spent.Currency);
        TotalTokensUsed += tokens;
    }
}
