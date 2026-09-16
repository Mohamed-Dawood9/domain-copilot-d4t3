using System;
using System.Threading.Tasks;
using DomainCopilot.Core.Enums;
using DomainCopilot.Core.Services.Contract;

namespace DomainCopilot.Service.Services;

public class CostGovernorService : ICostGovernorService
{
    public Task PreFlightCheckAsync(string userId, ModelTier tier)
    {
        throw new NotImplementedException();
    }

    public Task RecordSpendAsync(string userId, int tokens, ModelTier tier)
    {
        throw new NotImplementedException();
    }
}
