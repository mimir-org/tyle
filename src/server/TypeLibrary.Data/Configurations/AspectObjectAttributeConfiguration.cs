using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class AspectObjectAttributeConfiguration : IEntityTypeConfiguration<AspectObjectAttributeLibDm>
{
    public void Configure(EntityTypeBuilder<AspectObjectAttributeLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("AspectObject_Attribute");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();

        builder.HasOne(x => x.AspectObject).WithMany(y => y.AspectObjectAttributes).HasForeignKey(x => x.AspectObjectId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Attribute).WithMany(y => y.AttributeAspectObjects).HasForeignKey(x => x.AttributeId).OnDelete(DeleteBehavior.Cascade);
    }
}