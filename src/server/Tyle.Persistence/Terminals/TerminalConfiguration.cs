using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tyle.Application.Common;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class TerminalConfiguration : IEntityTypeConfiguration<TerminalType>
{
    private readonly ValueConverter<ICollection<string>, string> _valueConverter;
    private readonly ValueComparer<ICollection<string>> _valueComparer;

    public TerminalConfiguration(ValueConverter<ICollection<string>, string> valueConverter, ValueComparer<ICollection<string>> valueComparer)
    {
        _valueConverter = valueConverter;
        _valueComparer = valueComparer;
    }

    public void Configure(EntityTypeBuilder<TerminalType> builder)
    {
        builder.ToTable("Terminal");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.Version).IsRequired().HasMaxLength(StringLengthConstants.VersionLength);
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(StringLengthConstants.CreatedByLength);
        builder.Property(x => x.ContributedBy).IsRequired().HasConversion(_valueConverter, _valueComparer).HasMaxLength(StringLengthConstants.ContributedByLength);

        builder.Property(x => x.Notation).HasMaxLength(StringLengthConstants.NotationLength);
        builder.Property(x => x.Symbol).HasMaxLength(StringLengthConstants.IriLength);
        builder.Property(x => x.Aspect).HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
        builder.Property(x => x.Qualifier).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);

        builder
            .HasOne(e => e.Purpose)
            .WithMany()
            .HasForeignKey(e => e.PurposeId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder
            .HasOne(e => e.Medium)
            .WithMany()
            .HasForeignKey(e => e.MediumId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}