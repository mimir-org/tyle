using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data.Enums;

namespace TypeLibrary.Models.Configurations
{
    public class TypeAttributeConfiguration : IEntityTypeConfiguration<TypeAttribute>
    {
        public void Configure(EntityTypeBuilder<TypeAttribute> builder)
        {
            builder.ToTable("TypeAttribute");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);
            builder.Property(p => p.ParentId).HasColumnName("ParentId").IsRequired(false);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>();

            builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}