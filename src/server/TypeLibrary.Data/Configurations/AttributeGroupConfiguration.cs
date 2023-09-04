using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;


namespace TypeLibrary.Data.Configurations
{
    public class AttributeGroupConfiguration : IEntityTypeConfiguration<AttributeGroupDm>
    {
        public void Configure(EntityTypeBuilder<AttributeGroupDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.State).IsUnique(false);
            builder.ToTable("AttributeGroup");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Attribute).HasColumnName("Attribute").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
            builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);
        }
    }
}