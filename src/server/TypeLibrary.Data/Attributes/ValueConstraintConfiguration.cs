using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class ValueConstraintConfiguration : IEntityTypeConfiguration<ValueConstraint>
{
    public void Configure(EntityTypeBuilder<ValueConstraint> builder)
    {
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
