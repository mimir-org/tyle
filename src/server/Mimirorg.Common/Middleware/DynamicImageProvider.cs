using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Common.Middleware
{
    public class DynamicImageProvider
    {
        private readonly RequestDelegate _next;
        private const string PathCondition = "/symbol/";
        private const int CacheDays = 10;
        private static readonly Dictionary<string, string> Suffixes = new()
        {
            { ".png", "image/png" },
            { ".jpg", "image/jpg" },
            { ".svg", "image/svg+xml" }
        };

        public DynamicImageProvider(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke middleware image request
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="dataProvider">The data provider to resolve the base64 string</param>
        /// <returns>A completed task</returns>
        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context, IDynamicImageDataProvider dataProvider, IHttpContextAccessor contextAccessor)
        {
            try
            {
                var path = context.Request.Path;

                if (path.Value != null && IsImagePath(path) && path.Value.Contains(PathCondition))
                {
                    var id = Path.GetFileName(path).Split(".")[0];
                    var symbol = await dataProvider.GetSymbolDataAsync(id);

                    if (!string.IsNullOrWhiteSpace(symbol))
                    {
                        var buffer = Convert.FromBase64String(symbol);

                        if (buffer.Length > 1)
                        {
                            var suffix = Suffixes.FirstOrDefault(x => path.Value != null && string.Equals(x.Key, GetFileExtensions(path), StringComparison.OrdinalIgnoreCase));
                            context.Response.ContentLength = buffer.Length;
                            context.Response.ContentType = suffix.Value;

                            if (CacheDays > 0)
                            {
                                var expire = DateTime.UtcNow.AddDays(CacheDays).ToString("ddd, dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                context.Response.Headers.Add("Expires", expire + " GMT");
                            }

                            await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                        }
                        else
                            context.Response.StatusCode = 404;
                    }
                }
            }
            finally
            {
                if (!context.Response.HasStarted)
                    await _next(context);
            }
        }

        /// <summary>
        /// Check if path includes defined image path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsImagePath(PathString path)
        {
            if (path == null || !path.HasValue)
                return false;

            return Suffixes.Any(x => path.Value != null && path.Value.EndsWith(x.Key, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get the image file extension
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetFileExtensions(PathString path)
        {
            if (path == null || !path.HasValue)
                return null;

            return $".{Path.GetFileName(path).Split(".")[1]}";
        }
    }
}
