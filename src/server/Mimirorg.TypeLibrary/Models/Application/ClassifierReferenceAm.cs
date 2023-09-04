using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class ClassifierReferenceAm
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Iri { get; set; }
}