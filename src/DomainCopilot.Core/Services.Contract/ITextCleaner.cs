using System.Collections.Generic;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Core.Services.Contract;

public interface ITextCleaner
{
    IEnumerable<ExtractedSection> Clean(IEnumerable<ExtractedSection> sections);
}
