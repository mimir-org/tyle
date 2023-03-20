using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Enums;

public enum State
{
    //Draft
    [Display(Name = "Draft")]
    Draft = 0,

    //Company
    [Display(Name = "Approve Company")]
    ApproveCompany = 1,

    [Display(Name = "Approved Company")]
    ApprovedCompany = 2,

    //Global
    [Display(Name = "Approve Global")]
    ApproveGlobal = 3,

    [Display(Name = "Approved Global")]
    ApprovedGlobal = 4,

    //Delete
    [Display(Name = "Delete")]
    Delete = 5,

    [Display(Name = "Deleted")]
    Deleted = 6
}