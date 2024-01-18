using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Application.Common;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class ValueListEntryConfiguration : IEntityTypeConfiguration<ValueListEntry>
{
    public void Configure(EntityTypeBuilder<ValueListEntry> builder)
    {
        builder.ToTable("ValueListEntry");

        builder.Property(x => x.EntryValue).IsRequired().HasMaxLength(StringLengthConstants.ValueLength);
    }
}