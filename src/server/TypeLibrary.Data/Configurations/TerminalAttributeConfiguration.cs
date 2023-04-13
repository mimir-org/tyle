using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class TerminalAttributeConfiguration : IEntityTypeConfiguration<TerminalAttributeLibDm>
{
    public void Configure(EntityTypeBuilder<TerminalAttributeLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Terminal_Attribute");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(63);
    }
}