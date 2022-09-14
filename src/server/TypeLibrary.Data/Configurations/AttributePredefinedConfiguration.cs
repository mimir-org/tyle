using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Common.Converters;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class AttributePredefinedConfiguration : IEntityTypeConfiguration<AttributePredefinedLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributePredefinedLibDm> builder)
        {
            var stringConverter = new StringCollectionValueConverter();
            var stringComparer = new StringCollectionValueComparer();

            builder.HasKey(x => x.Key);
            builder.ToTable("AttributePredefined");
            builder.Property(p => p.Key).HasColumnName("Key").IsRequired().HasMaxLength(127);
            builder.Property(p => p.IsMultiSelect).HasColumnName("IsMultiSelect").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").HasMaxLength(255);
            builder.Property(p => p.TypeReferences).HasColumnName("TypeReferences");
            builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.ValueStringList).HasColumnName("ValueStringList").IsRequired(false).HasConversion(stringConverter, stringComparer);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
        }
    }
}