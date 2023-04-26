using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class QuantityDatumLibAm
{
    [Required]
    public string Name { get; set; }

    public string TypeReference { get; set; }

    [Required]
    public QuantityDatumType QuantityDatumType { get; set; }

    public string Description { get; set; }
}