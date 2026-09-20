using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Repository.Data;

namespace DomainCopilot.Repository.Repositories;

public class CaseRepository : GenericRepository<CitizenCase>, ICaseRepository
{
    public CaseRepository(AppDbContext context) : base(context)
    {
    }
}
