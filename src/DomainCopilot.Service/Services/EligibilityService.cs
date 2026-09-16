using System;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Services.Contract;

namespace DomainCopilot.Service.Services;

public class EligibilityService : IEligibilityService
{
    public Task<bool> EvaluateEligibilityAsync(CitizenCase citizenCase)
    {
        throw new NotImplementedException();
    }
}
