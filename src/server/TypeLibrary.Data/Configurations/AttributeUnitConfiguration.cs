using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class AttributeUnitConfiguration : IEntityTypeConfiguration<AttributeUnitLibDm>
{
    public void Configure(EntityTypeBuilder<AttributeUnitLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Attribute_Unit");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.IsDefault).HasColumnName("IsDefault").IsRequired();

        builder.HasOne(x => x.Unit).WithMany(y => y.UnitAttributes).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Attribute).WithMany(y => y.AttributeUnits).HasForeignKey(x => x.AttributeId).OnDelete(DeleteBehavior.NoAction);
    }
}