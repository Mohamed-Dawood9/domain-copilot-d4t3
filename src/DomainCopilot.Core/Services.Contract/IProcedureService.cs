using System.Threading.Tasks;
using DomainCopilot.Core.Entities;

namespace DomainCopilot.Core.Services.Contract;

public interface IProcedureService
{
    Task<string> ResolveProcedureAsync(CitizenCase citizenCase);
}
