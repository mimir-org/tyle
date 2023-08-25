using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;
using Mimirorg.Common.Converters;

namespace TypeLibrary.Data.Configurations;

public class BlockConfiguration : IEntityTypeConfiguration<BlockLibDm>
{
    public void Configure(EntityTypeBuilder<BlockLibDm> builder)
    {
        var stringConverter = new StringCollectionValueConverter();
        var stringComparer = new StringCollectionValueComparer();

        builder.HasKey(x => x.Id);
        //builder.HasIndex(x => x.FirstVersionId).IsUnique(false);
        //builder.HasIndex(x => x.State).IsUnique(false);
        //builder.HasIndex(x => new { x.State, x.Aspect }).IsUnique(false);
        builder.ToTable("Block");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(500);
        builder.Property(p => p.Version).HasColumnName("Version").IsRequired().HasMaxLength(10);
        builder.Property(p => p.CreatedOn).HasColumnName("CreatedOn").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(50);
        builder.Property(p => p.ContributedBy).HasColumnName("ContributedBy").IsRequired()
            .HasConversion(stringConverter, stringComparer).HasMaxLength(2000);
        builder.Property(p => p.LastUpdateOn).HasColumnName("LastUpdateOn").IsRequired();
        //builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired();
        //builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.Classifiers).HasColumnName("Classifiers").IsRequired().HasConversion(stringConverter, stringComparer).HasMaxLength(5000);
        builder.Property(p => p.Purpose).HasColumnName("Purpose").HasMaxLength(500);
        builder.Property(p => p.Notation).HasColumnName("Notation").HasMaxLength(50);
        builder.Property(p => p.Symbol).HasColumnName("Symbol").HasMaxLength(500);
        builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(20);
        
        //builder.Property(p => p.SelectedAttributePredefined).HasJsonConversion();

        //builder.HasOne(x => x.Rds).WithMany().HasForeignKey(x => x.RdsId).OnDelete(DeleteBehavior.SetNull);
    }
}