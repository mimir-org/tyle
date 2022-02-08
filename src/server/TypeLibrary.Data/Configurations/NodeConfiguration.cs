using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Configurations
{
    public class NodeConfiguration : IEntityTypeConfiguration<NodeLibDm>
    {
        public void Configure(EntityTypeBuilder<NodeLibDm> builder)
        {
            builder.Property(p => p.AttributeAspect).HasColumnName("AttributeAspect").IsRequired();
            builder.Property(p => p.SymbolId).HasColumnName("SymbolId").IsRequired();
        }
    }
}