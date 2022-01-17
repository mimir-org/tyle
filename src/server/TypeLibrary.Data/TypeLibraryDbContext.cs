using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Data.TypeEditor;
using Microsoft.EntityFrameworkCore;

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<AttributeCondition> AttributeConditions { get; set; }
        public virtual DbSet<AttributeFormat> AttributeFormats { get; set; }
        public virtual DbSet<AttributeQualifier> AttributeQualifiers { get; set; }
        public virtual DbSet<AttributeSource> AttributeSources { get; set; }
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
        public virtual DbSet<TypeAttribute> TypeAttributes { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

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
            modelBuilder.ApplyConfiguration(new TypeAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
        }
    }
}