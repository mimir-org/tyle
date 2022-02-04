using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Common.Converters;
using TypeLibrary.Models.Models.Data;

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
            builder.Property(p => p.Key).HasColumnName("Key").IsRequired();
            builder.Property(p => p.Values).HasColumnName("Values").IsRequired(false).HasConversion(stringConverter, stringComparer);
            builder.Property(p => p.IsMultiSelect).HasColumnName("IsMultiSelect").IsRequired();
        }
    }
}
