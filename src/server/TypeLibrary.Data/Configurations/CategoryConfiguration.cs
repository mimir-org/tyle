using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryLibDm>
{
    public void Configure(EntityTypeBuilder<CategoryLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Category");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(63);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(255);
    }
}