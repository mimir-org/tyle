using Mimirorg.Common.Enums;

namespace Mimirorg.Common.Models
{
    public class CacheInvalidation
    {
        public CacheKey Key { get; set; }
        public string Secret { get; set; }
    }
}
