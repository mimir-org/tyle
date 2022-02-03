using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class TransportConfiguration : IEntityTypeConfiguration<TransportLibDm>
    {
        public void Configure(EntityTypeBuilder<TransportLibDm> builder)
        {
            builder.Property(p => p.TerminalId).HasColumnName("Transport_TerminalId").IsRequired(false);
            
            builder.HasOne(x => x.TerminalDm).WithMany(y => y.Transports).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
