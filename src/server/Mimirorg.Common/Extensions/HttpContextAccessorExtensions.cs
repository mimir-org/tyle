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
        public static string GetUserEmail(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
        }

        /// <summary>
        /// Get the logged in user's database id
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <returns>The database id of current logged in user</returns>
        public static string GetUserId(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Get the logged in user
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <returns>The logged in user</returns>
        public static ClaimsPrincipal GetUser(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor?.HttpContext?.User;
        }
    }
}