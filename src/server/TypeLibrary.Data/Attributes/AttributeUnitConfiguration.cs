using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class AttributeUnitConfiguration : IEntityTypeConfiguration<AttributeUnitJoin>
{
    public void Configure(EntityTypeBuilder<AttributeUnitJoin> builder)
    {
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