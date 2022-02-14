using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums
{
    public enum Select
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Single select")]
        SingleSelect = 1,

        [Display(Name = "Multi select")]
        MultiSelect = 2
    }
}
