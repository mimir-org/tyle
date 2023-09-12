using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Configurations;

public class TerminalAttributeConfiguration : IEntityTypeConfiguration<TerminalAttributeTypeReference>
{
    public void Configure(EntityTypeBuilder<TerminalAttributeTypeReference> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Terminal_Attribute");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.MinCount).HasColumnName("MinCount").IsRequired();
        builder.Property(p => p.MaxCount).HasColumnName("MaxCount");
    }
}