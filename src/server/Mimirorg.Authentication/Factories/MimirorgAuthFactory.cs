using Microsoft.AspNetCore.Identity;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Constants;
using Mimirorg.Common.Extensions;

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
        yield return new IdentityRole { Id = MimirorgDefaultRoles.AdministratorRoleId, Name = MimirorgDefaultRoles.Administrator, NormalizedName = MimirorgDefaultRoles.Administrator.ResolveNormalizedName() };
        yield return new IdentityRole { Id = MimirorgDefaultRoles.AccountManagerRoleId, Name = MimirorgDefaultRoles.AccountManager, NormalizedName = MimirorgDefaultRoles.AccountManager.ResolveNormalizedName() };
        yield return new IdentityRole { Id = MimirorgDefaultRoles.ModeratorRoleId, Name = MimirorgDefaultRoles.Moderator, NormalizedName = MimirorgDefaultRoles.Moderator.ResolveNormalizedName() };
    }

    #endregion
}