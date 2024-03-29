using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Core.Blocks;

namespace Tyle.Persistence.Blocks;

public class BlockAttributeConfiguration : IEntityTypeConfiguration<BlockAttributeTypeReference>
{
    public void Configure(EntityTypeBuilder<BlockAttributeTypeReference> builder)
    {
        builder.ToTable("Block_Attribute");

        builder.HasKey(x => new { x.BlockId, x.AttributeId });

        builder.Property(x => x.MinCount).IsRequired();

        builder
            .HasOne(e => e.Block)
            .WithMany(e => e.Attributes)
            .HasForeignKey(e => e.BlockId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(e => e.AsPartOfAttributeGroup)
            .WithMany()
            .HasForeignKey(e => e.AttributeGroupId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}