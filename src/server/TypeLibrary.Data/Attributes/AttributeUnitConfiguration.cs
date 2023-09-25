using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TypeLibrary.Data.Attributes;

public class AttributeUnitConfiguration : IEntityTypeConfiguration<AttributeUnitDao>
{
    public void Configure(EntityTypeBuilder<AttributeUnitDao> builder)
    {
        builder
            .HasOne(e => e.Attribute)
            .WithMany(e => e.AttributeUnits)
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