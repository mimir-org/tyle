using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Configurations.Converters;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class PredefinedAttributeConfiguration : IEntityTypeConfiguration<PredefinedAttribute>
    {
        public void Configure(EntityTypeBuilder<PredefinedAttribute> builder)
        {
            var stringConverter = new StringCollectionValueConverter();
            var stringComparer = new StringCollectionValueComparer();

            builder.HasKey(x => x.Key);
            builder.ToTable("PredefinedAttribute");
            builder.Property(p => p.Key).HasColumnName("Key").IsRequired();
            builder.Property(p => p.Values).HasColumnName("Values").IsRequired(false).HasConversion(stringConverter, stringComparer);
            builder.Property(p => p.IsMultiSelect).HasColumnName("IsMultiSelect").IsRequired();
        }
    }
}
