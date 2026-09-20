using System;
using System.IO;
using System.Threading.Tasks;
using DomainCopilot.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomainCopilot.APIs.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AdminController : ControllerBase
{
    private readonly IIngestionService _ingestionService;

    public AdminController(IIngestionService ingestionService)
    {
        _ingestionService = ingestionService;
    }

    [HttpPost("ingest")]
    public async Task<IActionResult> IngestCorpus()
    {
        // Ideally this path should be configured in appsettings
        var corpusPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "corpus", "raw");
        if (!Directory.Exists(corpusPath))
        {
            return BadRequest($"Corpus directory not found at {corpusPath}");
        }

        var files = Directory.GetFiles(corpusPath, "*.*", SearchOption.AllDirectories);
        int processedCount = 0;

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file).ToLowerInvariant();
            if (extension != ".pdf" && extension != ".docx")
            {
                continue;
            }

            var fileName = Path.GetFileName(file);
            var sourceName = Path.GetFileNameWithoutExtension(file);
            var version = "1.0"; // Default, could parse from filename or metadata
            var section = "General"; // Default
            var effectiveDate = DateTime.UtcNow.Date; // Default

            await _ingestionService.ProcessFileAsync(file, sourceName, section, version, effectiveDate);
            processedCount++;
        }

        return Ok(new { Message = $"Ingestion triggered for {processedCount} files." });
    }
}
