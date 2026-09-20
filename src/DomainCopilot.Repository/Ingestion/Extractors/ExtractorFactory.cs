using System;
using DomainCopilot.Core.Services.Contract;

namespace DomainCopilot.Repository.Ingestion.Extractors;

public class ExtractorFactory : IExtractorFactory
{
    public IDocumentExtractor GetExtractor(string extension)
    {
        return extension.ToLowerInvariant() switch
        {
            ".pdf" => new PdfExtractor(),
            ".docx" => new DocxExtractor(),
            ".txt" => new TxtExtractor(),
            _ => throw new NotSupportedException($"File extension {extension} is not supported.")
        };
    }
}
