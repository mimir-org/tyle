using System.ComponentModel.DataAnnotations;

namespace Tyle.Application.Common.Requests;

public abstract class ReferenceRequest
{
    [Required]
    public string Name { get; }

    public string? Description { get; }

    [Required, ValidIri(ErrorMessage = "The IRI must be a valid absolute URI.")]
    public string Iri { get; }
}
