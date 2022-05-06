using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Mimirorg.Common.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        /// <summary>
        /// Get the logged in user's name
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <returns>The name of current logged in user</returns>
        public static string GetName(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor?.HttpContext?.User == null)
                return null;

            var user = contextAccessor.HttpContext.User;
            return user.Claims.FirstOrDefault(x => x.Type == "name")?.Value ??
                   user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Get the url of the running application
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <returns>Current running application url</returns>
        public static string GetApplicationUrl(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor?.HttpContext == null)
                return string.Empty;

            var displayUrl = contextAccessor.HttpContext.Request.GetDisplayUrl();
            var url = new Uri(displayUrl);

            return url.IsDefaultPort ? $"{url.Scheme}://{url.Host}" : $"{url.Scheme}://{url.Host}:{url.Port}";
        }
    }
}
