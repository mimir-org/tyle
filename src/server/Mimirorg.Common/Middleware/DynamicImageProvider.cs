using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Abstract;

namespace Mimirorg.Common.Middleware
{
    public class DynamicImageProvider
    {
        private readonly RequestDelegate _next;
        private const string PathSymbolCondition = "/symbol/";
        private const string PathLogoCondition = "/logo/";
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
        /// <param name="logoDataProvider"></param>
        /// <returns>A completed task</returns>
        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context, IDynamicSymbolDataProvider dataProvider, IDynamicLogoDataProvider logoDataProvider)
        {
            try
            {
                var path = context.Request.Path;

                if (path.Value != null && IsImagePath(path) && path.Value.Contains(PathSymbolCondition))
                {
                    var id = Path.GetFileName(path).Split(".")[0];
                    var symbol = await dataProvider.GetSymbolDataAsync(id);
                    await CreateResponse(symbol, path, context);
                }

                if (path.Value != null && IsImagePath(path) && path.Value.Contains(PathLogoCondition))
                {
                    var id = Path.GetFileName(path).Split(".")[0];

                    if (int.TryParse(id, out var intId))
                    {
                        var logo = await logoDataProvider.GetLogoDataAsync(intId);
                        await CreateResponse(logo, path, context);
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
        /// Create file response
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task CreateResponse(string base64, PathString path, HttpContext context)
        {
            if (!string.IsNullOrWhiteSpace(base64))
            {
                var buffer = Convert.FromBase64String(base64);

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