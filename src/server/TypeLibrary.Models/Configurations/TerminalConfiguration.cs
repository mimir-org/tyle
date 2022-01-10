using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class TerminalConfiguration : IEntityTypeConfiguration<Terminal>
    {
        public void Configure(EntityTypeBuilder<Terminal> builder)
        {
            builder.Property(p => p.TerminalCategoryId).HasColumnName("Terminal_CategoryId").IsRequired();
            builder.Property(p => p.TerminalTypeId).HasColumnName("TerminalTypeId").IsRequired(false);
            builder.HasOne(x => x.TerminalCategory).WithMany(y => y.Terminals).HasForeignKey(x => x.TerminalCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
