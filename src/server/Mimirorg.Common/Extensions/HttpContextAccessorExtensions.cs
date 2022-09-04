using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

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
        /// Get the logged in user's email
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <returns>The email of current logged in user</returns>
        public static string GetEmail(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor?.HttpContext?.User == null)
                return null;

            var user = contextAccessor.HttpContext.User;
            return user.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value ??
                   user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}