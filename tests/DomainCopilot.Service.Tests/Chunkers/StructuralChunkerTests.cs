using System;
using System.Collections.Generic;
using System.Linq;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.ValueObjects;
using DomainCopilot.Repository.Ingestion.Chunkers;
using Xunit;

namespace DomainCopilot.Service.Tests.Chunkers;

public class StructuralChunkerTests
{
    [Fact]
    public void ChunkDocument_FeeSchedule_InjectsEffectiveDate()
    {
        // Arrange
        var chunker = new StructuralChunker();
        var effectiveDate = new DateTime(2025, 1, 1);
        var document = new DocumentRecord("building-fee-schedule", "Fees", "1.0", 1, effectiveDate, ".pdf", "", "");

        var sections = new List<ExtractedSection>
        {
            new ExtractedSection("Schedule", "Application Fee: $50\nRenewal Fee: $100", 1)
        };

        // Act
        var chunks = chunker.ChunkDocument(document, sections).ToList();

        // Assert
        Assert.Equal(2, chunks.Count);
        Assert.Contains("[Effective Date: 2025-01-01] Application Fee: $50", chunks[0].Content);
        Assert.Contains("[Effective Date: 2025-01-01] Renewal Fee: $100", chunks[1].Content);
    }
}
