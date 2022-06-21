using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums
{
    public enum State
    {
        [Display(Name = "Draft")]
        Draft = 0,

        [Display(Name = "Deleted")]
        Deleted = 1,

        [Display(Name = "Approved Company")]
        ApprovedCompany = 2,

        [Display(Name = "Approved Global")]
        ApprovedGlobal = 3
    }
}