using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Configurations;
using TypeLibrary.Data.Models;
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data
{
    public class TypeLibraryDbContext : DbContext
    {
        public virtual DbSet<AttributeLibDm> Attribute { get; set; }
        public virtual DbSet<SymbolLibDm> Symbol { get; set; }
        public virtual DbSet<InterfaceLibDm> Interface { get; set; }
        public virtual DbSet<NodeTerminalLibDm> TerminalNode { get; set; }
        public virtual DbSet<AttributePredefinedLibDm> AttributePredefined { get; set; }
        public virtual DbSet<TerminalLibDm> Terminal { get; set; }
        public virtual DbSet<TransportLibDm> Transport { get; set; }
        public virtual DbSet<NodeLibDm> Node { get; set; }

        public TypeLibraryDbContext(DbContextOptions<TypeLibraryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AttributeConfiguration());
            modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
            modelBuilder.ApplyConfiguration(new SymbolConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceConfiguration());
            modelBuilder.ApplyConfiguration(new NodeTerminalConfiguration());
            modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
            modelBuilder.ApplyConfiguration(new TerminalConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new NodeConfiguration());
        }
    }
}