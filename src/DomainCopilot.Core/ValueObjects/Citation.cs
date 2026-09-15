namespace DomainCopilot.Core.ValueObjects;

public record Citation(string DocumentId, string Section, int Page, string QuotedSpan);
