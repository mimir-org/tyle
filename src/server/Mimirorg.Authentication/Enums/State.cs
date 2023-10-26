using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Enums;

public enum State
{
    [Display(Name = "Draft")]
    Draft = 0,

    [Display(Name = "Review")]
    Review = 1,

    [Display(Name = "Approved")]
    Approved = 2
}