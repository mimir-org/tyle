using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class AbstractReferenceAm : IValidatableObject
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Iri { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Uri.TryCreate(Iri, UriKind.Absolute, out var _))
        {
            yield return new ValidationResult("The provided IRI must be a valid Uri.");
        }
    }
}
