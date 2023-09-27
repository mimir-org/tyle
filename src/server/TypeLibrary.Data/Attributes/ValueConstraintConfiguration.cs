using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class ValueConstraintConfiguration : IEntityTypeConfiguration<ValueConstraint>
{
    public void Configure(EntityTypeBuilder<ValueConstraint> builder)
    {
        builder.ToTable("ValueConstraint");

        builder.HasKey(x => x.AttributeId);

        builder.Property(x => x.ConstraintType).IsRequired().HasConversion<string>();
        builder.Property(x => x.DataType).IsRequired().HasConversion<string>();
        builder.Property(x => x.MinValue).HasPrecision(38, 19);
        builder.Property(x => x.MaxValue).HasPrecision(38, 19);

        builder
            .HasOne(e => e.Attribute)
            .WithOne(e => e.ValueConstraint)
            .HasForeignKey<ValueConstraint>(e => e.AttributeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder
            .HasMany(e => e.ValueList)
            .WithOne(e => e.ValueConstraint)
            .HasForeignKey(e => e.ValueConstraintId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}