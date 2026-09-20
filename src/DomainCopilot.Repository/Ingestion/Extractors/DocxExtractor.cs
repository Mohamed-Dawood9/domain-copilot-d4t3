using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Repository.Ingestion.Extractors;

public class DocxExtractor : IDocumentExtractor
{
    public Task<IEnumerable<ExtractedSection>> ExtractAsync(Stream documentStream)
    {
        var sections = new List<ExtractedSection>();

        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(documentStream, false))
        {
            var body = wordDoc.MainDocumentPart?.Document.Body;
            if (body != null)
            {
                // In a real implementation we would iterate over elements and find headings
                // to split sections properly, and find PageBreaks for page numbers.
                var content = string.Join("\n", body.Elements<Paragraph>().Select(p => p.InnerText));
                sections.Add(new ExtractedSection("Main Document", content, 1));
            }
        }

        return Task.FromResult<IEnumerable<ExtractedSection>>(sections);
    }
}
