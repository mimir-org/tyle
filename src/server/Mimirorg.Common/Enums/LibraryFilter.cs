using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Enums
{
    public enum LibraryFilter
    {
        [Display(Name = "Node")]
        Node = 0,

        [Display(Name = "Transport")]
        Transport = 1,

        [Display(Name = "Interface")]
        Interface = 2
    }
}
