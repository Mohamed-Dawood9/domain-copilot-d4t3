using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Core.ValueObjects;

namespace DomainCopilot.Repository.Ingestion;

public class TextCleaningService : ITextCleaner
{
    public IEnumerable<ExtractedSection> Clean(IEnumerable<ExtractedSection> sections)
    {
        var cleanedSections = new List<ExtractedSection>();

        // Find repeated lines across pages that might be headers/footers
        var allLines = sections.SelectMany(s => s.Content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).ToList();

        // A naive way to find headers/footers: lines that appear on more than 30% of pages
        var totalPages = sections.Select(s => s.PageNumber).Distinct().Count();
        var lineFrequencies = new Dictionary<string, int>();

        foreach (var section in sections)
        {
            var lines = section.Content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(l => l.Trim())
                                       .Where(l => l.Length > 0)
                                       .Distinct(); // only count once per page/section

            foreach (var line in lines)
            {
                if (!lineFrequencies.ContainsKey(line))
                    lineFrequencies[line] = 0;
                lineFrequencies[line]++;
            }
        }

        var repeatedLines = lineFrequencies.Where(kvp => totalPages > 2 && kvp.Value > totalPages * 0.3)
                                           .Select(kvp => kvp.Key)
                                           .ToHashSet();

        foreach (var section in sections)
        {
            // Normalize line breaks
            var lines = section.Content.Split(new[] { '\r', '\n' }, StringSplitOptions.None);

            var keptLines = new List<string>();
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (repeatedLines.Contains(trimmed) && trimmed.Length > 0)
                {
                    continue; // skip header/footer
                }

                keptLines.Add(trimmed);
            }

            var joinedContent = string.Join("\n", keptLines);

            // Collapse excessive whitespace
            joinedContent = Regex.Replace(joinedContent, @"\n{3,}", "\n\n");
            joinedContent = Regex.Replace(joinedContent, @"[ \t]{2,}", " ");

            cleanedSections.Add(new ExtractedSection(section.SectionTitle, joinedContent.Trim(), section.PageNumber));
        }

        return cleanedSections;
    }
}
