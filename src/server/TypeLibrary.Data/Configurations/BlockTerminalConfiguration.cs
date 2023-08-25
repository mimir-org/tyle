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
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.MinCount).HasColumnName("MinCount").IsRequired();
        builder.Property(p => p.MaxCount).HasColumnName("MaxCount");
        builder.Property(p => p.Direction).HasColumnName("Direction").IsRequired().HasConversion<string>().HasMaxLength(20);

        builder.HasOne(x => x.Terminal).WithMany(y => y.TerminalBlocks).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Block).WithMany(y => y.BlockTerminals).HasForeignKey(x => x.BlockId).OnDelete(DeleteBehavior.Cascade);
    }
}