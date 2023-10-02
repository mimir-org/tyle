using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;


namespace TypeLibrary.Data.Configurations
{
    public class AttributeGroupAttributesConfiguration : IEntityTypeConfiguration<AttributeGroupAttributesLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributeGroupAttributesLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("AttributeGroupAttributes");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.AttributeGroupId).HasColumnName("AttributeGroupId");
            builder.Property(p => p.AttributeId).HasColumnName("AttributeId").IsRequired().HasMaxLength(63);
            builder.HasOne(x => x.AttributeGroup).WithMany(y => y.AttributeGroupAttributes).OnDelete(DeleteBehavior.Cascade);
        }
    }
}