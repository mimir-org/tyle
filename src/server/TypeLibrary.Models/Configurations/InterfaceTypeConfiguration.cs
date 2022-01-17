using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class InterfaceTypeConfiguration : IEntityTypeConfiguration<InterfaceType>
    {
        public void Configure(EntityTypeBuilder<InterfaceType> builder)
        {
            builder.Property(p => p.TerminalTypeId).HasColumnName("InterfaceType_TerminalTypeId").IsRequired(false);
            
            builder.HasOne(x => x.TerminalType).WithMany(y => y.InterfaceTypes).HasForeignKey(x => x.TerminalTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
