using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Configurations
{
    public class NodeConfiguration : IEntityTypeConfiguration<NodeLibDm>
    {
        public void Configure(EntityTypeBuilder<NodeLibDm> builder)
        {
            builder.Property(p => p.AttributeAspectId).HasColumnName("AttributeAspectId").IsRequired();
            builder.Property(p => p.BlobId).HasColumnName("BlobId").IsRequired();
        }
    }
}