using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class AttributeUnitMappingConfiguration : IEntityTypeConfiguration<AttributeUnitMapping>
{
    public void Configure(EntityTypeBuilder<AttributeUnitMapping> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Attribute_Unit");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();

        builder.HasOne(e => e.Attribute).WithMany(e => e.Units).HasForeignKey(e => e.AttributeId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne(e => e.Unit).WithMany().HasForeignKey(e => e.UnitId).OnDelete(DeleteBehavior.Cascade).IsRequired();
    }
}