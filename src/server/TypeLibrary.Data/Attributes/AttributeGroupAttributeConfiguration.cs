using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class AttributeGroupAttributeConfiguration : IEntityTypeConfiguration<AttributeGroupAttributeJoin>
{
    public void Configure(EntityTypeBuilder<AttributeGroupAttributeJoin> builder)
    {
        builder.ToTable("AttributeGroup_Attribute");

        builder.HasKey(x => new { x.AttributeGroupId, x.AttributeId });

        builder
            .HasOne(e => e.AttributeGroup)
            .WithMany(e => e.Attributes)
            .HasForeignKey(e => e.AttributeGroupId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}