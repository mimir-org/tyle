using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums
{
    public enum LogType
    {
        [Display(Name = "State")]
        State = 0,
        [Display(Name = "Create")]
        Create = 1,
        [Display(Name = "Update")]
        Update = 2,
    }
}