using System;
using System.Threading.Tasks;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Repository.Data;

namespace DomainCopilot.Repository.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICaseRepository Cases { get; }
    public IDocumentRepository Documents { get; }
    public IBudgetRepository Budgets { get; }

    public UnitOfWork(AppDbContext context, ICaseRepository cases, IDocumentRepository documents, IBudgetRepository budgets)
    {
        _context = context;
        Cases = cases;
        Documents = documents;
        Budgets = budgets;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
