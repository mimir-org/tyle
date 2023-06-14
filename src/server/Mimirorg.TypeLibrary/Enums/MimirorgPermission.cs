using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums;

[Flags]
public enum MimirorgPermission
{
    [Display(Name = "None")]
    None = 0,

    [Display(Name = "Read")]
    Read = 1,

    [Display(Name = "Write")]
    Write = 2 | Read,

    [Display(Name = "Delete")]
    Delete = 4 | Write | Read,

    [Display(Name = "Approve")]
    Approve = 8 | Delete | Write | Read,

    [Display(Name = "Manage")]
    Manage = 16 | Approve
}