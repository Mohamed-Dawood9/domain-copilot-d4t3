namespace DomainCopilot.Core.Errors;

public record BudgetExceededError()
    : DomainError("BudgetExceeded", "The user's token budget cannot cover the pre-flight estimate.");
