using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Repository.Data;

namespace DomainCopilot.Repository.Repositories;

public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
{
    public BudgetRepository(AppDbContext context) : base(context)
    {
    }
}
