using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Application.Common;
using Tyle.Core.Blocks;

namespace Tyle.Persistence.Blocks;

public class ConnectionPointConfiguration : IEntityTypeConfiguration<ConnectionPoint>
{
    public void Configure(EntityTypeBuilder<ConnectionPoint> builder)
    {
        builder.ToTable("ConnectionPoint");

        builder.Property(x => x.Identifier).IsRequired().HasMaxLength(StringLengthConstants.IdentifierLength);
    }
}
