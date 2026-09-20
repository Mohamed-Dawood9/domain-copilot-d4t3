using DomainCopilot.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCopilot.Repository.Data.Configurations;

public class ChunkConfiguration : IEntityTypeConfiguration<Chunk>
{
    public void Configure(EntityTypeBuilder<Chunk> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(c => c.DocumentRecord)
               .WithMany()
               .HasForeignKey(c => c.DocumentRecordId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
