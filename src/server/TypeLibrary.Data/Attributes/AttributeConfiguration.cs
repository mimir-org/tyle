using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Data.Attributes;

public class AttributeConfiguration : IEntityTypeConfiguration<AttributeType>
{
    private readonly ValueConverter<ICollection<string>, string> _valueConverter;
    private readonly ValueComparer<ICollection<string>> _valueComparer;

    public AttributeConfiguration(ValueConverter<ICollection<string>, string> valueConverter, ValueComparer<ICollection<string>> valueComparer)
    {
        _valueConverter = valueConverter;
        _valueComparer = valueComparer;
    }

    public void Configure(EntityTypeBuilder<AttributeType> builder)
    {
        builder.ToTable("Attribute");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.Version).IsRequired().HasMaxLength(StringLengthConstants.VersionLength);
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(StringLengthConstants.CreatedByLength);
        builder.Property(x => x.ContributedBy).IsRequired().HasConversion(_valueConverter, _valueComparer).HasMaxLength(StringLengthConstants.ContributedByLength);

        builder.Property(x => x.UnitMinCount).IsRequired();
        builder.Property(x => x.UnitMaxCount).IsRequired();
        builder.Property(x => x.ProvenanceQualifier).HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
        builder.Property(x => x.RangeQualifier).HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
        builder.Property(x => x.RegularityQualifier).HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
        builder.Property(x => x.ScopeQualifier).HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);

        builder
            .HasOne(e => e.Predicate)
            .WithMany()
            .HasForeignKey(e => e.PredicateId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}