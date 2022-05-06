using Microsoft.AspNetCore.Builder;

namespace Mimirorg.Common.Middleware
{
    public static class DynamicImageMiddleware
    {
        /// <summary>
        /// Application builder extensions to resolve image url
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDynamicImageMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DynamicImageProvider>();
        }
    }
}
