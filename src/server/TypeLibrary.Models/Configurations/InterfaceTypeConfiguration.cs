using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class InterfaceTypeConfiguration : IEntityTypeConfiguration<InterfaceDm>
    {
        public void Configure(EntityTypeBuilder<InterfaceDm> builder)
        {
            builder.Property(p => p.TerminalId).HasColumnName("Interface_TerminalId").IsRequired(false);
            
            builder.HasOne(x => x.TerminalDm).WithMany(y => y.Interfaces).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
