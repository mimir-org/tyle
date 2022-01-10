using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Project");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.IsSubProject).HasColumnName("IsSubProject").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.ProjectOwner).HasColumnName("ProjectOwner").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired();
            builder.Property(p => p.Updated).HasColumnName("Updated").IsRequired();
            builder.Property(p => p.Version).HasColumnName("Version").IsRequired();

            builder.HasMany(x => x.Nodes).WithOne(y => y.Project).HasForeignKey(y => y.ProjectId).IsRequired();
            builder.HasMany(x => x.Edges).WithOne(y => y.Project).HasForeignKey(y => y.ProjectId).IsRequired();
        }
    }
}
