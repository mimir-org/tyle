using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class TerminalAttributeConfiguration : IEntityTypeConfiguration<TerminalAttributeTypeReference>
{
    public void Configure(EntityTypeBuilder<TerminalAttributeTypeReference> builder)
    {
        builder.ToTable("Terminal_Attribute");

        builder.HasKey(x => new { x.TerminalId, x.AttributeId });

        builder.Property(x => x.MinCount).IsRequired();

        builder
            .HasOne(e => e.Terminal)
            .WithMany(e => e.Attributes)
            .HasForeignKey(e => e.TerminalId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(e => e.AsPartOfAttributeGroup)
            .WithMany()
            .HasForeignKey(e => e.AttributeGroupId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}