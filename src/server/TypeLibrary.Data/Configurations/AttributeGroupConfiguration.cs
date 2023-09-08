using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;


namespace TypeLibrary.Data.Configurations
{
    public class AttributeGroupConfiguration : IEntityTypeConfiguration<AttributeGroupLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributeGroupLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("AttributeGroup");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);
            builder.HasMany(p => p.Attribute).WithOne(p => p.AttributeGroup).OnDelete(DeleteBehavior.Cascade);
        }
    }
}