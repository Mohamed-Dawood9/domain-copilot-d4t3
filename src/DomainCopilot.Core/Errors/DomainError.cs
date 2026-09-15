namespace DomainCopilot.Core.Errors;

public abstract record DomainError(string Code, string Message);
