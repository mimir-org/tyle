using Microsoft.AspNetCore.Http;

namespace Mimirorg.Common.Extensions;

public static class HttpContextExtensions
{
    public static string GetBaseUrl(this IHttpContextAccessor contextAccessor)
    {
        var appBaseUrl = $"{contextAccessor?.HttpContext?.Request.Scheme}://{contextAccessor?.HttpContext?.Request.Host}{contextAccessor?.HttpContext?.Request.PathBase}";
        return appBaseUrl.TrimEnd('/');
    }
}