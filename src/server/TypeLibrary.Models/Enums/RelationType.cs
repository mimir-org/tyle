using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Enums
{
    public enum RelationType
    {
        [Display(Name = "Not Set")]
        NotSet = 0,

        [Display(Name = "Has Location")]
        HasLocation = 1,

        [Display(Name = "Part Of")]
        PartOf = 2,

        [Display(Name = "Fulfilled By")]
        FulfilledBy = 3
    }
}
