using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class InterfaceConfiguration : IEntityTypeConfiguration<InterfaceLibDm>
    {
        public void Configure(EntityTypeBuilder<InterfaceLibDm> builder)
        {
            builder.Property(p => p.TerminalId).HasColumnName("Interface_TerminalId").IsRequired(false);
            
            builder.HasOne(x => x.Terminal).WithMany(y => y.Interfaces).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
