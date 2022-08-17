using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums
{
    public enum AttributeType
    {
        [Display(Name = "Normal")]
        Normal = 0,

        [Display(Name = "Type")]
        Type = 1,

        [Display(Name = "Predefined")]
        Predefined = 2,
    }
}