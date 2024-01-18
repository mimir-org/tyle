using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tyle.Application.Common;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class AttributeGroupConfiguration : IEntityTypeConfiguration<AttributeGroup>
{
    private readonly ValueConverter<ICollection<string>, string> _valueConverter;
    private readonly ValueComparer<ICollection<string>> _valueComparer;

    public AttributeGroupConfiguration(ValueConverter<ICollection<string>, string> valueConverter, ValueComparer<ICollection<string>> valueComparer)
    {
        _valueConverter = valueConverter;
        _valueComparer = valueComparer;
    }

    public void Configure(EntityTypeBuilder<AttributeGroup> builder)
    {
        builder.ToTable("AttributeGroup");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(StringLengthConstants.CreatedByLength);
        builder.Property(x => x.ContributedBy).IsRequired().HasConversion(_valueConverter, _valueComparer).HasMaxLength(StringLengthConstants.ContributedByLength);
    }
}