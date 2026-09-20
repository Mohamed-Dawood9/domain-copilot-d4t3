using DomainCopilot.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCopilot.Repository.Data.Configurations;

public class CitizenCaseConfiguration : IEntityTypeConfiguration<CitizenCase>
{
    public void Configure(EntityTypeBuilder<CitizenCase> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
