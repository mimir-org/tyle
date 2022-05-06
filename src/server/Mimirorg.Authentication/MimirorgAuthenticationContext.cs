using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Configurations;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication
{
    public class MimirorgAuthenticationContext : IdentityDbContext<MimirorgUser, IdentityRole, string>
    {
        public DbSet<MimirorgToken> MimirorgTokens { get; set; }
        public DbSet<MimirorgCompany> MimirorgCompanies { get; set; }
        public DbSet<MimirorgHook> MimirorgHooks { get; set; }
        public IMimirorgAuthFactory AuthFactory { get; set; }

        public MimirorgAuthenticationContext(DbContextOptions<MimirorgAuthenticationContext> options, IMimirorgAuthFactory authFactory) : base(options)
        {
            AuthFactory = authFactory;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MimirorgTokenConfiguration());
            modelBuilder.ApplyConfiguration(new MimirorgCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new MimirorgHookConfiguration());
            var defaultRoles = AuthFactory.DefaultRoles.ToArray();
            modelBuilder.Entity<IdentityRole>().HasData(defaultRoles);
        }
    }
}
