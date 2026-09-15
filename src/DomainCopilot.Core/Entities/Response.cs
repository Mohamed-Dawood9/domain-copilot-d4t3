using System;

namespace DomainCopilot.Core.Entities;

public class Response : BaseEntity
{
    public Guid CitizenCaseId { get; private set; }
    public string DraftContent { get; private set; }
    public string FinalContent { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Response(Guid citizenCaseId, string draftContent)
    {
        CitizenCaseId = citizenCaseId;
        DraftContent = draftContent;
        FinalContent = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public void FinalizeResponse(string finalContent)
    {
        FinalContent = finalContent;
    }
}
