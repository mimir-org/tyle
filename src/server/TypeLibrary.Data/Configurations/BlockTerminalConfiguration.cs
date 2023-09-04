using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class BlockTerminalConfiguration : IEntityTypeConfiguration<BlockTerminalTypeReference>
{
    public void Configure(EntityTypeBuilder<BlockTerminalTypeReference> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Block_Terminal");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.MinCount).HasColumnName("MinCount").IsRequired();
        builder.Property(p => p.MaxCount).HasColumnName("MaxCount");
        builder.Property(p => p.Direction).HasColumnName("Direction").IsRequired().HasConversion<string>().HasMaxLength(20);
    }
}