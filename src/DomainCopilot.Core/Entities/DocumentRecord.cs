using System;

namespace DomainCopilot.Core.Entities;

public class DocumentRecord : BaseEntity
{
    public string Source { get; private set; }
    public string Section { get; private set; }
    public int Page { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public string Content { get; private set; }

    public DocumentRecord(string source, string section, int page, DateTime effectiveDate, string content)
    {
        Source = source;
        Section = section;
        Page = page;
        EffectiveDate = effectiveDate;
        Content = content;
    }
}
