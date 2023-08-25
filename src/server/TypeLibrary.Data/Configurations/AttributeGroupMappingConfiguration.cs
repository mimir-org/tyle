using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class AttributeGroupMappingConfiguration : IEntityTypeConfiguration<AttributeGroupMappingLibDm>
{
    public void Configure(EntityTypeBuilder<AttributeGroupMappingLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Attribute_Group_Mapping");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);

        builder.HasOne(x => x.Attribute).WithMany(y => y.AttributeGroups).HasForeignKey(x => x.AttributeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.AttributeGroup).WithMany(y => y.Attributes).HasForeignKey(x => x.AttributeGroupId).OnDelete(DeleteBehavior.Cascade);
    }
}