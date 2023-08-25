using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Common.Converters;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class AttributeGroupConfiguration : IEntityTypeConfiguration<AttributeGroupLibDm>
{
    public void Configure(EntityTypeBuilder<AttributeGroupLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Attribute_Group");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
    }
}