using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Models.Configurations
{
    public class TransportTypeConfiguration : IEntityTypeConfiguration<TransportType>
    {
        public void Configure(EntityTypeBuilder<TransportType> builder)
        {
            builder.Property(p => p.TerminalTypeId).HasColumnName("TransportType_TerminalTypeId").IsRequired(false);
            
            builder.HasOne(x => x.TerminalType).WithMany(y => y.TransportTypes).HasForeignKey(x => x.TerminalTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
