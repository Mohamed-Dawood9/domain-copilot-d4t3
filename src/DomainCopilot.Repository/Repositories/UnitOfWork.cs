using System;
using System.Threading.Tasks;
using DomainCopilot.Core.Repositories.Contract;

namespace DomainCopilot.Repository.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICaseRepository Cases { get; }
    public IDocumentRepository Documents { get; }
    public IBudgetRepository Budgets { get; }

    public UnitOfWork(ICaseRepository cases, IDocumentRepository documents, IBudgetRepository budgets)
    {
        Cases = cases;
        Documents = documents;
        Budgets = budgets;
    }

    public Task<int> CompleteAsync()
    {
        // Stub implementation
        return Task.FromResult(0);
    }

    public void Dispose()
    {
        // Stub implementation
        GC.SuppressFinalize(this);
    }
}
