using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Core.Services.Contract;

public interface IDocumentExtractor
{
    Task<IEnumerable<ExtractedSection>> ExtractAsync(Stream documentStream);
}
