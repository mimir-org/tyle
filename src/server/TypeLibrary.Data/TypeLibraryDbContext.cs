using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Configurations;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<AttributeConditionLibDm> AttributeCondition { get; set; }
        public virtual DbSet<AttributeFormatLibDm> AttributeFormat { get; set; }
        public virtual DbSet<AttributeQualifierLibDm> AttributeQualifier { get; set; }
        public virtual DbSet<AttributeSourceLibDm> AttributeSource { get; set; }
        public virtual DbSet<AttributeLibDm> Attribute { get; set; }
        public virtual DbSet<BlobLibDm> Blob { get; set; }
        public virtual DbSet<InterfaceLibDm> Interface { get; set; }
        public virtual DbSet<LibraryTypeLibDm> LibraryType { get; set; }
        public virtual DbSet<TerminalNodeLibDm> TerminalNode { get; set; }
        public virtual DbSet<AttributePredefinedLibDm> AttributePredefined { get; set; }
        public virtual DbSet<PurposeLibDm> Purpose { get; set; }
        public virtual DbSet<RdsCategoryLibDm> RdsCategory { get; set; }
        public virtual DbSet<RdsLibDm> Rds { get; set; }
        public virtual DbSet<SimpleLibDm> Simple { get; set; }
        public virtual DbSet<TerminalLibDm> Terminal { get; set; }
        public virtual DbSet<TransportLibDm> Transport { get; set; }
        public virtual DbSet<AttributeAspectLibDm> AttributeAspect { get; set; }
        public virtual DbSet<UnitLibDm> Unit { get; set; }
        public virtual DbSet<NodeLibDm> Node { get; set; }

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
            modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
            modelBuilder.ApplyConfiguration(new BlobConfiguration());
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