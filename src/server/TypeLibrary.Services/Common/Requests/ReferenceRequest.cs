using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Services.Common.Requests;

public abstract class ReferenceRequest
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required, ValidIri(ErrorMessage = "The IRI must be a valid absolute URI.")]
    public required string Iri { get; set; }
}
