using System;
using DomainCopilot.Core.Entities;

namespace DomainCopilot.Core.Specifications;

public class RulesForServiceSpecification : BaseSpecification<EligibilityRule>
{
    public RulesForServiceSpecification(Guid serviceId)
        : base(x => x.ServiceId == serviceId)
    {
    }
}
