using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Enums;

[Flags]
public enum MimirorgPermission
{
    [Display(Name = "None")]
    None = 0,

    [Display(Name = "Read")]
    Read = 1,

    [Display(Name = "Write")]
    Write = 2 | Read,

    [Display(Name = "Approve")]
    Approve = 4 | Write,

    [Display(Name = "Manage")]
    Manage = 8 | Approve
}