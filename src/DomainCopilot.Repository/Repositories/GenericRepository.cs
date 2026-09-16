using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainCopilot.Core.Repositories.Contract;

namespace DomainCopilot.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<T>> ListAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }
}
