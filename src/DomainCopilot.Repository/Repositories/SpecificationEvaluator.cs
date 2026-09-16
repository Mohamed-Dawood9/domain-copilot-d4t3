using System;
using System.Linq;
using DomainCopilot.Core.Specifications;

namespace DomainCopilot.Repository.Repositories;

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        throw new NotImplementedException();
    }
}
