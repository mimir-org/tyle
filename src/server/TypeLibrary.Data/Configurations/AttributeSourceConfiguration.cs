using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class AttributeSourceConfiguration : IEntityTypeConfiguration<AttributeSourceLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributeSourceLibDm> builder)
        {
            builder.ToTable("AttributeSource");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false).HasMaxLength(511);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false).HasDefaultValue(null).HasMaxLength(31);
            builder.Property(p => p.Updated).HasColumnName("Updated").IsRequired(false).HasDefaultValue(null);
        }
    }
}