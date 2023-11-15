using System.Security.Claims;
using HtmlAgilityPack;
using Microsoft.Identity.Web;

namespace Tyle.Api.Common
{
    public static class AccessToAction
    {

        public static bool HasUserAccessToDoCurrentOperation(ClaimsPrincipal? user, HttpMethod action)
        {
            if (user == null)
                return false;

            if (action == HttpMethod.Post && (user.IsInRole("Contributor") || user.IsInRole("Administrator") || user.IsInRole("Reviewer")))
                return true;

            if (action == HttpMethod.Put && (user.IsInRole("Administrator") || user.IsInRole("Reviewer")))
                return true;

            if (action == HttpMethod.Patch && (user.IsInRole("Administrator") || user.IsInRole("Reviewer")))
                return true;

            return false;
        }
    }
}


