using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class AttributeAspectConfiguration : IEntityTypeConfiguration<AttributeAspectLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributeAspectLibDm> builder)
        {
            builder.ToTable("AttributeAspect");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(511);
            builder.Property(p => p.Iri).HasColumnName("Iri").HasMaxLength(255);
            builder.Property(p => p.ContentReferences).HasColumnName("ContentReferences");
            builder.Property(p => p.Deleted).HasColumnName("Deleted").IsRequired().HasDefaultValue(0);
            builder.Property(p => p.ParentId).HasColumnName("ParentId").HasMaxLength(127);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();

            builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}