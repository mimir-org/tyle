using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class AttributeUnitConfiguration : IEntityTypeConfiguration<AttributeUnitJoin>
{
    public void Configure(EntityTypeBuilder<AttributeUnitJoin> builder)
    {
        builder.ToTable("Attribute_Unit");

        builder.HasKey(x => new { x.AttributeId, x.UnitId });

        builder
            .HasOne(e => e.Attribute)
            .WithMany(e => e.Units)
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(e => e.Unit)
            .WithMany()
            .HasForeignKey(e => e.UnitId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}