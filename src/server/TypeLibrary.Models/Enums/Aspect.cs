using System;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Enums
{
    [Flags]
    public enum Aspect
    {
        [Display(Name = "Not set")]
        NotSet = 0,

        [Display(Name = "None")]
        None = 1,

        [Display(Name = "Function")]
        Function = 2,

        [Display(Name = "Product")]
        Product = 4,

        [Display(Name = "Location")]
        Location = 8
    }
}
