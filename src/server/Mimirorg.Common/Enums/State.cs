using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Enums;

public enum State
{
    //Draft
    [Display(Name = "Draft")]
    Draft = 0,
    
    //Approve
    [Display(Name = "Approve")]
    Approve = 1,

    [Display(Name = "Approved")]
    Approved = 2,

    //Delete
    [Display(Name = "Delete")]
    Delete = 3,

    [Display(Name = "Deleted")]
    Deleted = 4
}