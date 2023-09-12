using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Attributes;

namespace Mimirorg.TypeLibrary.Models.Application;

public class ReferenceRequest
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required, ValidIri]
    public string Iri { get; set; }
}
