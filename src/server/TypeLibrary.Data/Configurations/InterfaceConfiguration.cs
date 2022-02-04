using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Configurations
{
    public class InterfaceConfiguration : IEntityTypeConfiguration<InterfaceLibDm>
    {
        public void Configure(EntityTypeBuilder<InterfaceLibDm> builder)
        {
            builder.Property(p => p.TerminalId).HasColumnName("Interface_TerminalId").IsRequired(false);
            
            builder.HasOne(x => x.TerminalDm).WithMany(y => y.Interfaces).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
