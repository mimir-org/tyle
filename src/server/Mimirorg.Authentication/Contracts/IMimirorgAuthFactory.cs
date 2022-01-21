using Microsoft.AspNetCore.Identity;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgAuthFactory
    {
        ICollection<IdentityRole> DefaultRoles { get; }
    }
}
