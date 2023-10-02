using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class BlockTerminalConfiguration : IEntityTypeConfiguration<BlockTerminalLibDm>
{
    public void Configure(EntityTypeBuilder<BlockTerminalLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Block_Terminal");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(63);
        builder.Property(p => p.MinQuantity).HasColumnName("MinQuantity").IsRequired().HasDefaultValue(1);
        builder.Property(p => p.MaxQuantity).HasColumnName("MaxQuantity").IsRequired().HasDefaultValue(int.MaxValue);
        builder.Property(p => p.ConnectorDirection).HasColumnName("ConnectorDirection").IsRequired().HasConversion<string>().HasMaxLength(31);

        builder.HasOne(x => x.Terminal).WithMany(y => y.TerminalBlocks).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Block).WithMany(y => y.BlockTerminals).HasForeignKey(x => x.BlockId).OnDelete(DeleteBehavior.Cascade);
    }
}