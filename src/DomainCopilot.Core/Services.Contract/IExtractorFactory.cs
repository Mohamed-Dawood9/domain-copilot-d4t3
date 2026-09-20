using System;

namespace DomainCopilot.Core.Services.Contract;

public interface IExtractorFactory
{
    IDocumentExtractor GetExtractor(string extension);
}
