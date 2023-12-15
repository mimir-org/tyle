using Microsoft.AspNetCore.Identity;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Constants;

namespace Mimirorg.Authentication.Factories;

public class MimirorgAuthFactory : IMimirorgAuthFactory
{
    public ICollection<IdentityRole> DefaultRoles { get; }

    public MimirorgAuthFactory()
    {
        DefaultRoles = CreateDefaultRoles().ToList();
    }

    #region Private methods

    private static IEnumerable<IdentityRole> CreateDefaultRoles()
    {
        yield return new IdentityRole
        {
            Name = MimirorgDefaultRoles.Administrator,
            NormalizedName = MimirorgDefaultRoles.Administrator.Normalize()
        };
        yield return new IdentityRole
        {
            Name = MimirorgDefaultRoles.Reviewer,
            NormalizedName = MimirorgDefaultRoles.Reviewer.Normalize()
        };
        yield return new IdentityRole
        {
            Name = MimirorgDefaultRoles.Contributor,
            NormalizedName = MimirorgDefaultRoles.Contributor.Normalize()
        };
        yield return new IdentityRole
        {
            Name = MimirorgDefaultRoles.Reader,
            NormalizedName = MimirorgDefaultRoles.Reader.Normalize()
        };
    }

    #endregion
}