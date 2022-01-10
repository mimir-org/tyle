using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Models;

namespace Mimirorg.Authentication
{
    public class AuthenticationContext : IdentityDbContext<MimirorgUser, IdentityRole, string>
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {

        }
    }
}
