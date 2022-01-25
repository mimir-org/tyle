using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Format> Formats { get; set; }
        public virtual DbSet<Qualifier> Qualifiers { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<AttributeType> AttributeTypes { get; set; }
        public virtual DbSet<BlobData> BlobData { get; set; }
        public virtual DbSet<InterfaceType> InterfaceTypes { get; set; }
        public virtual DbSet<LibraryType> LibraryTypes { get; set; }
        public virtual DbSet<NodeTypeTerminalType> NodeTypeTerminalTypes { get; set; }
        public virtual DbSet<PredefinedAttribute> PredefinedAttributes { get; set; }
        public virtual DbSet<Purpose> Purposes { get; set; }
        public virtual DbSet<RdsCategory> RdsCategories { get; set; }
        public virtual DbSet<Rds> Rds { get; set; }
        public virtual DbSet<SimpleType> SimpleTypes { get; set; }
        public virtual DbSet<TerminalType> TerminalTypes { get; set; }
        public virtual DbSet<TransportType> TransportTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

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
            modelBuilder.ApplyConfiguration(new AttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BlobDataConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NodeTypeTerminalTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PredefinedAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new PurposeConfiguration());
            modelBuilder.ApplyConfiguration(new RdsCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RdsConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransportTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
        }
    }
}