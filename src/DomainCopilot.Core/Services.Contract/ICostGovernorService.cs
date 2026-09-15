using System.Threading.Tasks;
using DomainCopilot.Core.Enums;

namespace DomainCopilot.Core.Services.Contract;

public interface ICostGovernorService
{
    Task PreFlightCheckAsync(string userId, ModelTier tier);
    Task RecordSpendAsync(string userId, int tokens, ModelTier tier);
}
