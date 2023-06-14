using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Enums;

public enum State
{
    [Display(Name = "Draft")]
    Draft = 0,

    [Display(Name = "Approve")]
    Approve = 1,

    [Display(Name = "Approved")]
    Approved = 2,

    [Display(Name = "Delete")]
    Delete = 3,

    [Display(Name = "Deleted")]
    Deleted = 4,

    [Display(Name = "Rejected")]
    Rejected = 5
}