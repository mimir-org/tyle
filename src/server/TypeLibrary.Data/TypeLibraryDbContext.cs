using Mb.Models.Configurations;
using Mb.Models.Data;
using Mb.Models.Data.Enums;
using Mb.Models.Data.TypeEditor;
using Microsoft.EntityFrameworkCore;

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<Rds> Rds { get; set; }
        public virtual DbSet<AttributeType> AttributeTypes { get; set; }
        public virtual DbSet<LibraryType> LibraryTypes { get; set; }
        public virtual DbSet<TerminalType> TerminalTypes { get; set; }
        public virtual DbSet<EnumBase> Enums { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<AttributeCondition> AttributeConditions { get; set; }
        public virtual DbSet<AttributeFormat> AttributeFormats { get; set; }
        public virtual DbSet<AttributeQualifier> AttributeQualifiers { get; set; }
        public virtual DbSet<AttributeSource> AttributeSources { get; set; }
        public virtual DbSet<BuildStatus> BuildStatuses { get; set; }
        public virtual DbSet<RdsCategory> RdsCategories { get; set; }
        public virtual DbSet<TerminalCategory> TerminalCategories { get; set; }
        public virtual DbSet<NodeType> NodeTypes { get; set; }
        public virtual DbSet<TransportType> TransportTypes { get; set; }
        public virtual DbSet<InterfaceType> InterfaceTypes { get; set; }
        public virtual DbSet<SimpleType> SimpleTypes { get; set; }
        public virtual DbSet<NodeTypeTerminalType> NodeTypeTerminalTypes { get; set; }
        public virtual DbSet<TypeAttribute> TypeAttributes { get; set; }
        public virtual DbSet<PredefinedAttribute> PredefinedAttributes { get; set; }
        public virtual DbSet<BlobData> BlobData { get; set; }
        public virtual DbSet<Purpose> BlobCategories { get; set; }

        public TypeLibraryDbContext(DbContextOptions<TypeLibraryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RdsConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EnumBaseConfiguration());
            modelBuilder.ApplyConfiguration(new NodeTypeTerminalTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransportTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PredefinedAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new BlobDataConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleTypeConfiguration());
        }
    }
}
