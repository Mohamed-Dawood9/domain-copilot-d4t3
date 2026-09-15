using System.Threading.Tasks;
using DomainCopilot.Core.Entities;

namespace DomainCopilot.Core.Services.Contract;

public interface IEligibilityService
{
    Task<bool> EvaluateEligibilityAsync(CitizenCase citizenCase);
}
