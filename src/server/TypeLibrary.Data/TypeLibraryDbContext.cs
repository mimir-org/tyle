using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Configurations;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<AttributeConditionLibDm> Conditions { get; set; }
        public virtual DbSet<AttributeFormatLibDm> Formats { get; set; }
        public virtual DbSet<AttributeQualifierLibDm> Qualifiers { get; set; }
        public virtual DbSet<AttributeSourceLibDm> Sources { get; set; }
        public virtual DbSet<AttributeLibDm> Attributes { get; set; }
        public virtual DbSet<BlobLibDm> BlobData { get; set; }
        public virtual DbSet<InterfaceLibDm> InterfaceTypes { get; set; }
        public virtual DbSet<LibraryTypeLibDm> LibraryTypes { get; set; }
        public virtual DbSet<TerminalNodeLibDm> NodeTypeTerminalTypes { get; set; }
        public virtual DbSet<AttributePredefinedLibDm> PredefinedAttributes { get; set; }
        public virtual DbSet<PurposeLibDm> Purposes { get; set; }
        public virtual DbSet<RdsCategoryLibDm> RdsCategories { get; set; }
        public virtual DbSet<RdsLibDm> Rds { get; set; }
        public virtual DbSet<SimpleLibDm> SimpleTypes { get; set; }
        public virtual DbSet<TerminalLibDm> Terminal { get; set; }
        public virtual DbSet<TransportLibDm> Transport { get; set; }
        public virtual DbSet<AttributeAspectLibDm> Locations { get; set; }
        public virtual DbSet<UnitLibDm> Units { get; set; }
        public virtual DbSet<NodeLibDm> Nodes { get; set; }

        public TypeLibraryDbContext(DbContextOptions<TypeLibraryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AttributeConditionConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeFormatConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeQualifierConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeSourceConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeConfiguration());
            modelBuilder.ApplyConfiguration(new BlobDataConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalNodeConfiguration());
            modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
            modelBuilder.ApplyConfiguration(new PurposeConfiguration());
            modelBuilder.ApplyConfiguration(new RdsCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RdsConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeAspectConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
        }
    }
}