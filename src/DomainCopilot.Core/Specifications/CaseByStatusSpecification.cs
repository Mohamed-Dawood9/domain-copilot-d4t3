using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Enums;

namespace DomainCopilot.Core.Specifications;

public class CaseByStatusSpecification : BaseSpecification<CitizenCase>
{
    public CaseByStatusSpecification(CaseStatus status)
        : base(x => x.Status == status)
    {
    }
}
