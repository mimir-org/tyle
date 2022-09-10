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
        Write = 2 | Read,

        [Display(Name = "ReadWrite")]
        ReadWrite = 3,

        [Display(Name = "Delete")]
        Delete = 4 | ReadWrite,

        [Display(Name = "ReadWriteDelete")]
        ReadWriteDelete = 7,

        [Display(Name = "Approve")]
        Approve = 8 | ReadWriteDelete,

        [Display(Name = "Manage")]
        Manage = 16 | Approve
    }
}