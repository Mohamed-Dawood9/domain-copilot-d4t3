using System;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Services.Contract;

namespace DomainCopilot.Service.Services;

public class Orchestrator : IOrchestrator
{
    public Task ProcessCaseAsync(CitizenCase citizenCase)
    {
        throw new NotImplementedException();
    }
}
