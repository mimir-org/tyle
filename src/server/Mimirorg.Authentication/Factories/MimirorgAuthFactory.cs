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
        yield return new IdentityRole(MimirorgDefaultRoles.Administrator);
        yield return new IdentityRole(MimirorgDefaultRoles.Reviewer);
        yield return new IdentityRole(MimirorgDefaultRoles.Contributor);
        yield return new IdentityRole(MimirorgDefaultRoles.Reader);
    }

    #endregion
}