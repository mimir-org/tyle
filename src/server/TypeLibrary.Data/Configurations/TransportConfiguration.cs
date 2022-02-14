using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class TransportConfiguration : IEntityTypeConfiguration<TransportLibDm>
    {
        public void Configure(EntityTypeBuilder<TransportLibDm> builder)
        {
            builder.Property(p => p.TerminalId).HasColumnName("Transport_TerminalId").IsRequired(false);
            
            builder.HasOne(x => x.Terminal).WithMany(y => y.Transports).HasForeignKey(x => x.TerminalId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
