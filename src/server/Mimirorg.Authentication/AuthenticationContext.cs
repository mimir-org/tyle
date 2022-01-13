using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Configurations;
using Mimirorg.Authentication.Models;

namespace Mimirorg.Authentication
{
    public class AuthenticationContext : IdentityDbContext<MimirorgUser, IdentityRole, string>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            //modelBuilder.ApplyConfiguration(new UnitConfiguration());

            //var defaultRoles = AuthFactory.DefaultRoles.ToArray();
            //modelBuilder.Entity<IdentityRole>().HasData(defaultRoles);
        }
    }
}
