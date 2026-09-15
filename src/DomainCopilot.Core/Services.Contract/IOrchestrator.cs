using System.Threading.Tasks;
using DomainCopilot.Core.Entities;

namespace DomainCopilot.Core.Services.Contract;

public interface IOrchestrator
{
    Task ProcessCaseAsync(CitizenCase citizenCase);
}
