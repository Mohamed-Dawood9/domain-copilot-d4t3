using System;
using DomainCopilot.Core.Entities;

namespace DomainCopilot.Core.Specifications;

public class DocumentsEffectiveAsOfDateSpecification : BaseSpecification<DocumentRecord>
{
    public DocumentsEffectiveAsOfDateSpecification(DateTime date)
        : base(x => x.EffectiveDate <= date)
    {
        AddOrderByDescending(x => x.EffectiveDate);
    }
}
