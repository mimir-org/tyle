using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Configurations;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<ConditionLibDm> Conditions { get; set; }
        public virtual DbSet<FormatLibDm> Formats { get; set; }
        public virtual DbSet<QualifierLibDm> Qualifiers { get; set; }
        public virtual DbSet<SourceLibDm> Sources { get; set; }
        public virtual DbSet<AttributeLibDm> Attributes { get; set; }
        public virtual DbSet<BlobLibDm> BlobData { get; set; }
        public virtual DbSet<InterfaceLibDm> InterfaceTypes { get; set; }
        public virtual DbSet<TypeLibDm> LibraryTypes { get; set; }
        public virtual DbSet<NodeTerminalLibDm> NodeTypeTerminalTypes { get; set; }
        public virtual DbSet<AttributePredefinedLibDm> PredefinedAttributes { get; set; }
        public virtual DbSet<PurposeLibDm> Purposes { get; set; }
        public virtual DbSet<RdsCategoryLibDm> RdsCategories { get; set; }
        public virtual DbSet<RdsLibDm> Rds { get; set; }
        public virtual DbSet<SimpleLibDm> SimpleTypes { get; set; }
        public virtual DbSet<TerminalLibDm> TerminalTypes { get; set; }
        public virtual DbSet<TransportLibDm> TransportTypes { get; set; }
        public virtual DbSet<AttributeTypeLibDm> Locations { get; set; }
        public virtual DbSet<UnitLibDm> Units { get; set; }

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
            modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
            modelBuilder.ApplyConfiguration(new PurposeConfiguration());
            modelBuilder.ApplyConfiguration(new RdsCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RdsConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
        }
    }
}