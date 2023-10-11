using System.Security.Claims;
using Mimirorg.Authentication.Constants;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Client;

namespace Mimirorg.Authentication.Extensions;

public static class RolePermissionExtensions
{
    /// <summary>
    /// Resolves the role names for the user
    /// </summary>
    /// <param name="roles">A list of roles for the user</param>
    /// <param name="claims">A list of claim for the user</param>
    /// <param name="companies">A collection of all the registered companies</param>
    /// <param name="permissions">A collection of all permissions</param>
    /// <returns>A collection of role names</returns>
    public static ICollection<string> ResolveRoles(IEnumerable<string> roles, IEnumerable<Claim> claims, ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions)
    {
        var resolvedRoles = new List<string>();

        foreach (var role in roles)
        {
            switch (role)
            {
                case "Administrator":
                    resolvedRoles.Add("Global administrator");
                    break;
                case "Account Manager":
                    resolvedRoles.Add("Global account manager");
                    break;
                case "Moderator":
                    resolvedRoles.Add("Global moderator");
                    break;
            }
        }

        return resolvedRoles;
    }

    /// <summary>
    /// Resolves permissions for the user
    /// </summary>
    /// <param name="roles">A list of roles for the user</param>
    /// <param name="claims">A list of claim for the user</param>
    /// <param name="companies">A collection of all the registered companies</param>
    /// <param name="permissions">A collection of all permissions</param>
    /// <returns>A collection of permission names</returns>
    public static Dictionary<int, MimirorgPermission> ResolvePermissions(ICollection<string> roles, ICollection<Claim> claims, ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions)
    {
        var resolvedPermissions = new Dictionary<int, MimirorgPermission>();

        if (!companies.Any())
        {
            return resolvedPermissions;
        }

        // Administrator or Account Manager role should give full permission to all companies
        if (roles.Any(x => x is "Administrator" or "Account Manager"))
        {
            foreach (var company in companies)
            {
                resolvedPermissions.Add(company.Id, MimirorgPermission.Manage);
            }
        }

        var userCompanyClaims = claims.Where(x => companies.Any(y => x.Type == y.Id.ToString())).ToList();
        foreach (var claim in userCompanyClaims)
        {
            var company = companies.FirstOrDefault(x => x.Id.ToString() == claim.Type);
            var permission = permissions.FirstOrDefault(x => x.Name == claim.Value);

            if (company == null || permission == null) continue;

            if (resolvedPermissions.All(x => x.Key != company.Id))
            {
                resolvedPermissions.Add(company.Id, (MimirorgPermission) permission.Id);
            }
            else if (resolvedPermissions.FirstOrDefault(x => x.Key == company.Id).Value < (MimirorgPermission) permission.Id)
            {
                resolvedPermissions.Remove(company.Id);
                resolvedPermissions.Add(company.Id, (MimirorgPermission) permission.Id);
            }
        }

        var highestPermission = resolvedPermissions.Any() ? resolvedPermissions.Values.Max() : MimirorgPermission.None;
        resolvedPermissions.Add(CompanyConstants.AnyCompanyId, highestPermission);

        return resolvedPermissions;
    }
}