using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Blocks;

namespace TypeLibrary.Data.Blocks;

public class BlockTerminalTypeReferenceConfiguration : IEntityTypeConfiguration<BlockTerminalTypeReference>
{
    public void Configure(EntityTypeBuilder<BlockTerminalTypeReference> builder)
    {
        builder.ToTable("Block_Terminal");

        builder.HasKey(x => new { x.BlockId, x.TerminalId, x.Direction });

        builder.Property(x => x.Direction).IsRequired().HasConversion<string>();
        builder.Property(x => x.MinCount).IsRequired();

        builder
            .HasOne(e => e.Block)
            .WithMany(e => e.Terminals)
            .HasForeignKey(e => e.BlockId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(e => e.Terminal)
            .WithMany()
            .HasForeignKey(e => e.TerminalId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}