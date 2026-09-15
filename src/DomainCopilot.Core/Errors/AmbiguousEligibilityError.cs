namespace DomainCopilot.Core.Errors;

public record AmbiguousEligibilityError()
    : DomainError("AmbiguousEligibility", "The LLM identified conflicting rules in the documents.");
