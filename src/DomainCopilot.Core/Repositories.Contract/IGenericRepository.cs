using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainCopilot.Core.Repositories.Contract;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
