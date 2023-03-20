using System.Security.Claims;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Client;

public class MimirorgUserCm
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Dictionary<int, MimirorgPermission> Permissions { get; set; }
    public ICollection<string> Roles { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Purpose { get; set; }

    /// <summary>
    /// Resolves the role names for the user
    /// </summary>
    /// <param name="roles">A list of roles for the user</param>
    /// <param name="claims">A list of claim for the user</param>
    /// <param name="companies">A collection of all the registered companies</param>
    /// <param name="permissions">A collection of all permissions</param>
    /// <returns>A collection of role names</returns>
    public void ResolveRoles(IEnumerable<string> roles, IEnumerable<Claim> claims, ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions)
    {
        Roles = RolePermissionExtensions.ResolveRoles(roles, claims, companies, permissions);
    }

    /// <summary>
    /// Resolves permissions for the user
    /// </summary>
    /// <param name="roles">A list of roles for the user</param>
    /// <param name="claims">A list of claim for the user</param>
    /// <param name="companies">A collection of all the registered companies</param>
    /// <param name="permissions">A collection of all permissions</param>
    /// <returns>A collection of permission names</returns>
    [TSExclude]
    public void ResolvePermissions(ICollection<string> roles, ICollection<Claim> claims, ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions)
    {
        Permissions = RolePermissionExtensions.ResolvePermissions(roles, claims, companies, permissions);
    }
}