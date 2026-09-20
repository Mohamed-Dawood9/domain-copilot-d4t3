using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Repository.Ingestion.Extractors;

public class TxtExtractor : IDocumentExtractor
{
    public async Task<IEnumerable<ExtractedSection>> ExtractAsync(Stream documentStream)
    {
        using var reader = new StreamReader(documentStream);
        var content = await reader.ReadToEndAsync();

        return new List<ExtractedSection>
        {
            new ExtractedSection("Main Document", content, 1)
        };
    }
}
