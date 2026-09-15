using System.Collections.Generic;
using System.Threading.Tasks;
using DomainCopilot.Core.Enums;

namespace DomainCopilot.Core.Services.Contract;

public interface ILlmProvider
{
    Task<string> CompleteAsync(string prompt, ModelTier tier);
    IAsyncEnumerable<string> StreamAsync(string prompt, ModelTier tier);
    // Placeholder for actual Tool type implementation
    Task<string> CallWithToolsAsync(string prompt, IEnumerable<object> tools, ModelTier tier);
}
