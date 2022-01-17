using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data.Enums;

namespace TypeLibrary.Models.Configurations
{
    public class AttributeSourceConfiguration : IEntityTypeConfiguration<AttributeSource>
    {
        public void Configure(EntityTypeBuilder<AttributeSource> builder)
        {
            builder.ToTable("AttributeSource");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);
        }
    }
}