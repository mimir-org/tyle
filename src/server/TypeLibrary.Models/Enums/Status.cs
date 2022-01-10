using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Enums
{
    public enum Status
    {
        [Display(Name = "Not Set")]
        NotSet = 0,

        [Display(Name = "Draft")]
        Draft = 1,

        [Display(Name = "Approved")]
        Approved = 2
    }
}
