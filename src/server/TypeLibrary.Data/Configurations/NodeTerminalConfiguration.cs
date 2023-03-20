using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class NodeTerminalConfiguration : IEntityTypeConfiguration<NodeTerminalLibDm>
{
    public void Configure(EntityTypeBuilder<NodeTerminalLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Node_Terminal");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.MinQuantity).HasColumnName("MinQuantity").IsRequired().HasDefaultValue(1);
        builder.Property(p => p.MaxQuantity).HasColumnName("MaxQuantity").IsRequired().HasDefaultValue(int.MaxValue);
        builder.Property(p => p.ConnectorDirection).HasColumnName("ConnectorDirection").IsRequired().HasConversion<string>().HasMaxLength(31);

        builder.HasOne(x => x.Terminal).WithMany(y => y.TerminalNodes).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Node).WithMany(y => y.NodeTerminals).HasForeignKey(x => x.NodeId).OnDelete(DeleteBehavior.NoAction);
    }
}