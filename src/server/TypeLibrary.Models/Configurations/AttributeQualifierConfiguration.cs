using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data.Enums;

namespace TypeLibrary.Models.Configurations
{
    public class AttributeQualifierConfiguration : IEntityTypeConfiguration<AttributeQualifier>
    {
        public void Configure(EntityTypeBuilder<AttributeQualifier> builder)
        {
            builder.ToTable("AttributeQualifier");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);
        }
    }
}