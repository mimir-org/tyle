using System.ComponentModel.DataAnnotations;

namespace Tyle.Application.Common.Requests;

public abstract class ReferenceRequest
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required, ValidIri(ErrorMessage = "The IRI must be a valid absolute URI.")]
    public string Iri { get; set; }
}
