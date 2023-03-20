using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums;

public enum ObjectType
{
    [Display(Name = "Not set")]
    NotSet = 0,

    [Display(Name = "Object Block")]
    ObjectBlock = 1
}