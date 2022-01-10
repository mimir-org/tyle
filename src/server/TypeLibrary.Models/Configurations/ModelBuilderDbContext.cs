using Microsoft.EntityFrameworkCore;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Models.Configurations
{
    public class ModelBuilderDbContext : DbContext
    {
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<Edge> Edges { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Connector> Connectors { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<Terminal> Terminals { get; set; }
        public virtual DbSet<Rds> Rds { get; set; }
        public virtual DbSet<AttributeType> AttributeTypes { get; set; }
        public virtual DbSet<LibraryType> LibraryTypes { get; set; }
        public virtual DbSet<CollaborationPartner> CollaborationPartners { get; set; }
        public virtual DbSet<TerminalType> TerminalTypes { get; set; }
        public virtual DbSet<EnumBase> Enums { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<AttributeCondition> AttributeConditions { get; set; }
        public virtual DbSet<AttributeFormat> AttributeFormats { get; set; }
        public virtual DbSet<AttributeQualifier> AttributeQualifiers { get; set; }
        public virtual DbSet<AttributeSource> AttributeSources { get; set; }
        //public virtual DbSet<BuildStatus> BuildStatuses { get; set; }
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
        public virtual DbSet<Transport> Transports { get; set; }
        public virtual DbSet<Interface> Interfaces { get; set; }
        public virtual DbSet<Simple> Simples { get; set; }

        public ModelBuilderDbContext(DbContextOptions<ModelBuilderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new NodeConfiguration());
            modelBuilder.ApplyConfiguration(new EdgeConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeConfiguration());
            modelBuilder.ApplyConfiguration(new ConnectorConfiguration());
            modelBuilder.ApplyConfiguration(new RdsConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CollaborationPartnerConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EnumBaseConfiguration());
            modelBuilder.ApplyConfiguration(new RelationConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalConfiguration());
            modelBuilder.ApplyConfiguration(new NodeTypeTerminalTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransportTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PredefinedAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new BlobDataConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleConfiguration());
            modelBuilder.ApplyConfiguration(new PurposeConfiguration());
            modelBuilder.ApplyConfiguration(new VersionConfiguration());
        }
    }
}
