using DomainCopilot.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCopilot.Repository.Data.Configurations;

public class TokenLedgerEntryConfiguration : IEntityTypeConfiguration<TokenLedgerEntry>
{
    public void Configure(EntityTypeBuilder<TokenLedgerEntry> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Cost, c =>
        {
            c.Property(p => p.Amount).HasColumnName("CostAmount");
            c.Property(p => p.Currency).HasColumnName("CostCurrency").HasMaxLength(3);
        });
    }
}
