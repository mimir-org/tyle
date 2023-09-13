using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Attributes;

namespace Mimirorg.TypeLibrary.Models.Application;

public abstract class ReferenceRequest
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required, ValidIri(ErrorMessage = "The Iri must be a valid Uri value.")]
    public string Iri { get; set; }
}
