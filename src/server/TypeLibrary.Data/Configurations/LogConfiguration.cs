using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class LogConfiguration : IEntityTypeConfiguration<LogLibDm>
{
    public void Configure(EntityTypeBuilder<LogLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.ObjectId).IsUnique(false);
        builder.HasIndex(x => x.ObjectFirstVersionId).IsUnique(false);
        builder.HasIndex(x => x.ObjectType).IsUnique(false);
        builder.HasIndex(x => x.LogType).IsUnique(false);
        builder.HasIndex(x => new { x.ObjectId, x.ObjectFirstVersionId, x.ObjectType, x.LogType }).IsUnique(false);
        builder.ToTable("Log");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.ObjectId).HasColumnName("ObjectId").IsRequired().HasMaxLength(63);
        builder.Property(p => p.ObjectFirstVersionId).HasColumnName("ObjectFirstVersionId").IsRequired(false).HasMaxLength(63);
        builder.Property(p => p.ObjectVersion).HasColumnName("ObjectVersion").IsRequired(false).HasMaxLength(7);
        builder.Property(p => p.ObjectType).HasColumnName("ObjectType").IsRequired().HasMaxLength(63);
        builder.Property(p => p.ObjectName).HasColumnName("ObjectName").IsRequired().HasMaxLength(127);
        builder.Property(p => p.Created).HasColumnName("Created").IsRequired().HasMaxLength(63);
        builder.Property(p => p.CreatedBy).HasColumnName("User").IsRequired().HasMaxLength(127);
        builder.Property(p => p.LogType).HasColumnName("LogType").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.LogTypeValue).HasColumnName("LogTypeValue").IsRequired().HasMaxLength(255);
    }
}