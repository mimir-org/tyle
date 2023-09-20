using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tyle.Persistence.Attributes;

public class ValueConstraintConfiguration : IEntityTypeConfiguration<ValueConstraintDao>
{
    public void Configure(EntityTypeBuilder<ValueConstraintDao> builder)
    {
        builder
            .HasOne(e => e.Attribute)
            .WithOne(e => e.ValueConstraint)
            .HasForeignKey<ValueConstraintDao>(e => e.AttributeId)
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
