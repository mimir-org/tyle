using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Services.Common.Requests;

public abstract class RdlObjectRequest
{
    [Required, MaxLength(StringLengthConstants.NameLength)]
    public required string Name { get; set; }

    [MaxLength(StringLengthConstants.DescriptionLength)]
    public string? Description { get; set; }

    [Required, ValidIri(ErrorMessage = "The IRI must be a valid absolute URI."), MaxLength(StringLengthConstants.IriLength)]
    public required string Iri { get; set; }
}