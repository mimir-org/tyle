using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Constants;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Extensions;

public static class HttpContextExtensions
{
    public static bool HasPermission(this HttpContext context, MimirorgPermission permission, string value)
    {
        // If the user is in administrator role, always return true
        if (context.User.IsInRole(MimirorgDefaultRoles.Administrator))
            return true;

        // If manage flag and is in account manager role, always return true 
        if (MimirorgPermission.Manage.HasFlag(permission) && context.User.IsInRole(MimirorgDefaultRoles.AccountManager))
            return true;

        if (value == null)
            return false;

        var userPermission = GetUserPermission(context, value);
        return userPermission.HasFlag(permission);
    }

    private static MimirorgPermission GetUserPermission(this HttpContext context, string value)
    {
        var allClaimsForUser = context.User.Claims.ToList();
        var propertyValues = allClaimsForUser.Where(x => x.Type.Equals(value)).Select(x => x.Value).ToList();
        var claimsPropertyPermissions = new List<MimirorgPermission>();
        foreach (var propertyValue in propertyValues)
        {
            if (Enum.TryParse(propertyValue, out MimirorgPermission p))
                claimsPropertyPermissions.Add(p);
        }
        return claimsPropertyPermissions.ConvertToFlag();
    }

}