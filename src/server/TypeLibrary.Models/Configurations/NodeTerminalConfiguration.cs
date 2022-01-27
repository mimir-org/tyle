using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class NodeTerminalConfiguration : IEntityTypeConfiguration<NodeTerminalDm>
    {
        public void Configure(EntityTypeBuilder<NodeTerminalDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Node_Terminal");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Number).HasColumnName("Number").IsRequired().HasDefaultValue(1);
            builder.Property(p => p.ConnectorType).HasColumnName("Connector").IsRequired().HasConversion<string>();

            builder.HasOne(x => x.TerminalDm).WithMany(y => y.Nodes).HasForeignKey(x => x.TerminalTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.NodeDm).WithMany(y => y.TerminalTypes).HasForeignKey(x => x.NodeTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
