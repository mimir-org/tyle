using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums
{
    public enum LibraryTypeFilter
    {
        [Display(Name = "Node")]
        Node = 0,

        [Display(Name = "Transport")]
        Transport = 1,

        [Display(Name = "Interface")]
        Interface = 2
    }
}