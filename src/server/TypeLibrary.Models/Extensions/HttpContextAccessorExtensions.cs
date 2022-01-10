using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TypeLibrary.Models.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetName(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor?.HttpContext?.User == null)
                return null;

            var user = contextAccessor.HttpContext.User;
            return user.Claims.FirstOrDefault(x => x.Type == "name")?.Value ??
                   user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
