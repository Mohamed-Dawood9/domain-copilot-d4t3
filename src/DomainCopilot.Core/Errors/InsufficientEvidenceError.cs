namespace DomainCopilot.Core.Errors;

public record InsufficientEvidenceError()
    : DomainError("InsufficientEvidence", "Missing required documents to prove an entitlement.");
