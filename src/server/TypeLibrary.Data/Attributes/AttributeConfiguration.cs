using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tyle.Persistence.Attributes;

public class AttributeConfiguration : IEntityTypeConfiguration<AttributeDao>
{
    public void Configure(EntityTypeBuilder<AttributeDao> builder)
    {
        builder
            .HasOne(e => e.Predicate)
            .WithMany()
            .HasForeignKey(e => e.PredicateId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}