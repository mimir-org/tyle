using System.Security.Claims;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.TypeLibrary.Extensions;

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

        if (resolvedRoles.Any())
        {
            return resolvedRoles;
        }


        if (!companies.Any())
        {
            return resolvedRoles;
        }

        var userCompanyClaims = claims.Where(x => companies.Any(y => x.Type == y.Id.ToString())).ToList();
        if (!userCompanyClaims.Any())
        {
            return resolvedRoles;
        }

        foreach (var claim in userCompanyClaims)
        {
            var company = companies.FirstOrDefault(x => x.Id.ToString() == claim.Type);
            var permission = permissions.FirstOrDefault(x => x.Name == claim.Value);
            if (company != null && permission != null)
            {
                resolvedRoles.Add($"{company.DisplayName ?? company.Name} {(MimirorgPermission) permission.Id}");
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

        // Moderator role should give delete permission to all companies
        else if (roles.Any(x => x is "Moderator"))
        {
            foreach (var company in companies)
            {
                resolvedPermissions.Add(company.Id, MimirorgPermission.Delete);
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

        var highestPermission = resolvedPermissions.Values.Max();
        resolvedPermissions.Add(CompanyConstants.AnyCompanyId, highestPermission);

        return resolvedPermissions;
    }
}