using System;

namespace DomainCopilot.Core.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string GovernmentDepartment { get; private set; }

    public Service(string name, string description, string governmentDepartment)
    {
        Name = name;
        Description = description;
        GovernmentDepartment = governmentDepartment;
    }
}
