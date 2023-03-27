using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class SymbolLibAm
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Data { get; set; }

    public string TypeReference { get; set; }
}