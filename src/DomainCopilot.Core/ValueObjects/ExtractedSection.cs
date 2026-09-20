namespace DomainCopilot.Core.ValueObjects;

public class ExtractedSection
{
    public string SectionTitle { get; private set; }
    public string Content { get; private set; }
    public int PageNumber { get; private set; }

    public ExtractedSection(string sectionTitle, string content, int pageNumber)
    {
        SectionTitle = sectionTitle;
        Content = content;
        PageNumber = pageNumber;
    }
}
