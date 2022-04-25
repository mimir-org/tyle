using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Enums
{
    public enum CacheKey
    {
        [Display(Name = "Node")]
        Node = 0,

        [Display(Name = "Qualifier")]
        Qualifier = 1
    }
}