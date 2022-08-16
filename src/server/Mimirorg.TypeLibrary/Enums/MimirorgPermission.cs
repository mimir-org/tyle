using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums
{
    [Flags]
    public enum MimirorgPermission
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Read")]
        Read = 1,

        [Display(Name = "Write")]
        Write = 2,

        [Display(Name = "Approve")]
        Approve = 3,

        [Display(Name = "Delete")]
        Delete = 6,

        [Display(Name = "Manage")]
        Manage = 12
    }
}