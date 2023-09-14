using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common.Validation;

namespace Tyle.Application.Common.Requests;

public abstract class ReferenceRequest
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required, ValidIri(ErrorMessage = "The Iri must be a valid Uri value.")]
    public string Iri { get; set; }
}
