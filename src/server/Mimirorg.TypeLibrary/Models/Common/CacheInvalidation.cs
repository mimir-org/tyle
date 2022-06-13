using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Common
{
    public class CacheInvalidation
    {
        public CacheKey Key { get; set; }
        public string Secret { get; set; }
    }
}
