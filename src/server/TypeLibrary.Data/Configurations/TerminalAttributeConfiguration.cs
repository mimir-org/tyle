using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

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

        builder.HasOne(x => x.Terminal).WithMany(y => y.TerminalAttributes).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Attribute).WithMany(y => y.AttributeTerminals).HasForeignKey(x => x.AttributeId).OnDelete(DeleteBehavior.Cascade);
    }
}