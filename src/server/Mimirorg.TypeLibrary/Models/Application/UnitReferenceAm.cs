using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class UnitReferenceAm
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Symbol { get; set; }

    [Required]
    public string Iri { get; set; }
}