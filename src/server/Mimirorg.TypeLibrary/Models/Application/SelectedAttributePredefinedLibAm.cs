using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class SelectedAttributePredefinedLibAm
{
    [Required]
    public string Key { get; set; }

    public string TypeReference { get; set; }

    [Required]
    public bool IsMultiSelect { get; set; }

    [Required]
    public Dictionary<string, bool> Values { get; set; }
}