using System;
using System.Threading.Tasks;

namespace DomainCopilot.Core.Repositories.Contract;

public interface IUnitOfWork : IDisposable
{
    ICaseRepository Cases { get; }
    IDocumentRepository Documents { get; }
    IBudgetRepository Budgets { get; }

    Task<int> CompleteAsync();
}
