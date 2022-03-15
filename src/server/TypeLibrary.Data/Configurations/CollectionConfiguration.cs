using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class CollectionConfiguration : IEntityTypeConfiguration<CollectionLibDm>
    {
        public void Configure(EntityTypeBuilder<CollectionLibDm> builder)
        {
            builder.ToTable("Collection");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false).HasMaxLength(511);
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired(false).HasDefaultValue(null).HasMaxLength(127);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false).HasDefaultValue(null).HasMaxLength(63);
            builder.Property(p => p.Updated).HasColumnName("Updated").IsRequired(false).HasDefaultValue(null);
            
        }
    }
}