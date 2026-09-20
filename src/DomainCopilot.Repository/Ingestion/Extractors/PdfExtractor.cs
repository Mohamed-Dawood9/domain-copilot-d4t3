using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Core.ValueObjects;
using UglyToad.PdfPig;

namespace DomainCopilot.Repository.Ingestion.Extractors;

public class PdfExtractor : IDocumentExtractor
{
    public Task<IEnumerable<ExtractedSection>> ExtractAsync(Stream documentStream)
    {
        var sections = new List<ExtractedSection>();

        using (var document = PdfDocument.Open(documentStream))
        {
            foreach (var page in document.GetPages())
            {
                var content = page.Text;

                // For a more advanced implementation, we would extract fonts and sizes 
                // to determine actual Section Titles. For this implementation, we use Page level chunks.
                sections.Add(new ExtractedSection($"Page {page.Number}", content, page.Number));
            }
        }

        return Task.FromResult<IEnumerable<ExtractedSection>>(sections);
    }
}
