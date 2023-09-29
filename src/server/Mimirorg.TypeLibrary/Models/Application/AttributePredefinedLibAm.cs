using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class AttributePredefinedLibAm
{
    [Required]
    public string Key { get; set; }

    public string TypeReference { get; set; }

    [Required]
    public Aspect Aspect { get; set; }

    [Required]
    public bool IsMultiSelect { get; set; }

    [Required]
    public ICollection<string> ValueStringList { get; set; }
}