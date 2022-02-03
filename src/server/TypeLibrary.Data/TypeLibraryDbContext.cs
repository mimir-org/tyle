using TypeLibrary.Models.Configurations;

using Microsoft.EntityFrameworkCore;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<ConditionDm> Conditions { get; set; }
        public virtual DbSet<FormatDm> Formats { get; set; }
        public virtual DbSet<QualifierDm> Qualifiers { get; set; }
        public virtual DbSet<SourceDm> Sources { get; set; }
        public virtual DbSet<AttributeDm> Attributes { get; set; }
        public virtual DbSet<BlobDm> BlobData { get; set; }
        public virtual DbSet<InterfaceDm> InterfaceTypes { get; set; }
        public virtual DbSet<TypeDm> LibraryTypes { get; set; }
        public virtual DbSet<NodeTerminalDm> NodeTypeTerminalTypes { get; set; }
        public virtual DbSet<PredefinedAttributeDm> PredefinedAttributes { get; set; }
        public virtual DbSet<PurposeDm> Purposes { get; set; }
        public virtual DbSet<RdsCategoryDm> RdsCategories { get; set; }
        public virtual DbSet<RdsDm> Rds { get; set; }
        public virtual DbSet<SimpleDm> SimpleTypes { get; set; }
        public virtual DbSet<TerminalDm> TerminalTypes { get; set; }
        public virtual DbSet<TransportDm> TransportTypes { get; set; }
        public virtual DbSet<LocationDm> Locations { get; set; }
        public virtual DbSet<UnitDm> Units { get; set; }

        public TypeLibraryDbContext(DbContextOptions<TypeLibraryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ConditionConfiguration());
            modelBuilder.ApplyConfiguration(new FormatConfiguration());
            modelBuilder.ApplyConfiguration(new QualifierConfiguration());
            modelBuilder.ApplyConfiguration(new SourceConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeConfiguration());
            modelBuilder.ApplyConfiguration(new BlobDataConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryConfiguration());
            modelBuilder.ApplyConfiguration(new NodeTerminalConfiguration());
            modelBuilder.ApplyConfiguration(new PredefinedAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new PurposeConfiguration());
            modelBuilder.ApplyConfiguration(new RdsCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RdsConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}