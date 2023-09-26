using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class ValueListEntryConfiguration : IEntityTypeConfiguration<ValueListEntry>
{
    public void Configure(EntityTypeBuilder<ValueListEntry> builder)
    {
        builder.ToTable("ValueListEntry");

        builder.Property(x => x.EntryValue).IsRequired();
    }
}