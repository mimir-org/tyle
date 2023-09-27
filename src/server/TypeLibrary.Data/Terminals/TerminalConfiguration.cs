using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Data.Terminals;

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

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Version).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.ContributedBy).IsRequired().HasConversion(_valueConverter, _valueComparer);

        builder.Property(x => x.Aspect).HasConversion<string>();
        builder.Property(x => x.Qualifier).IsRequired().HasConversion<string>();

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