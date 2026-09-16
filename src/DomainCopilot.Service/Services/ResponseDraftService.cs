using System;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Services.Contract;

namespace DomainCopilot.Service.Services;

public class ResponseDraftService : IResponseDraftService
{
    public Task<Response> DraftResponseAsync(CitizenCase citizenCase)
    {
        throw new NotImplementedException();
    }
}
