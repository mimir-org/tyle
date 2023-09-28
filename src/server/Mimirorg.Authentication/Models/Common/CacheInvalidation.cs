using Mimirorg.Authentication.Enums;

namespace Mimirorg.Authentication.Models.Common;

public class CacheInvalidation
{
    public CacheKey Key { get; set; }
    public string Secret { get; set; }
}