using System.ComponentModel.DataAnnotations;
using Tyle.Application.Attributes;

namespace Tyle.Application.Common.Requests;

public class AttributeTypeReferenceRequest : IValidatableObject
{
    [Required, Range(0, int.MaxValue, ErrorMessage = "The attribute min count cannot be negative.")]
    public int MinCount { get; set; }

    public int? MaxCount { get; set; }

    [Required]
    public Guid AttributeId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MinCount > MaxCount)
        {
            yield return new ValidationResult("The attribute min count cannot be larger than the attribute max count.");
        }

        var attributeRepository = (IAttributeRepository) validationContext.GetService(typeof(IAttributeRepository))!;

        var attribute = attributeRepository.Get(AttributeId).Result;

        if (attribute == null)
        {
            yield return new ValidationResult($"Couldn't find an attribute with id {AttributeId}.");
        }
    }
}