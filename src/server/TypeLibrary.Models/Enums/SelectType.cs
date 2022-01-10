using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Enums
{
    public enum SelectType
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Single select")]
        SingleSelect = 1,

        [Display(Name = "Multi select")]
        MultiSelect = 2
    }
}
