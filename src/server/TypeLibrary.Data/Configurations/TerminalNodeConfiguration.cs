using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Configurations
{
    public class TerminalNodeConfiguration : IEntityTypeConfiguration<TerminalNodeLibDm>
    {
        public void Configure(EntityTypeBuilder<TerminalNodeLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Terminal_Node");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Number).HasColumnName("Number").IsRequired().HasDefaultValue(1);
            builder.Property(p => p.ConnectorType).HasColumnName("Connector").IsRequired().HasConversion<string>();

            builder.HasOne(x => x.Terminal).WithMany(y => y.TerminalNodes).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Node).WithMany(y => y.Terminals).HasForeignKey(x => x.NodeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
