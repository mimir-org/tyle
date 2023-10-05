using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Core.Blocks;

namespace Tyle.Persistence.Blocks;

public class BlockClassifierConfiguration : IEntityTypeConfiguration<BlockClassifierJoin>
{
    public void Configure(EntityTypeBuilder<BlockClassifierJoin> builder)
    {
        builder.ToTable("Block_Classifier");

        builder.HasKey(x => new { x.BlockId, x.ClassifierId });

        builder
            .HasOne(e => e.Block)
            .WithMany(e => e.Classifiers)
            .HasForeignKey(e => e.BlockId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(e => e.Classifier)
            .WithMany()
            .HasForeignKey(e => e.ClassifierId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}