using DomainCopilot.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCopilot.Repository.Data.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Allocated, a =>
        {
            a.Property(p => p.Amount).HasColumnName("AllocatedAmount");
            a.Property(p => p.Currency).HasColumnName("AllocatedCurrency").HasMaxLength(3);
        });

        builder.OwnsOne(x => x.Spent, s =>
        {
            s.Property(p => p.Amount).HasColumnName("SpentAmount");
            s.Property(p => p.Currency).HasColumnName("SpentCurrency").HasMaxLength(3);
        });
    }
}
