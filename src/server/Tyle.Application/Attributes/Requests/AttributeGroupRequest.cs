using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;

namespace Tyle.Application.Attributes.Requests;

public class AttributeGroupRequest : IValidatableObject
{
    [Required, MaxLength(StringLengthConstants.NameLength)]
    public required string Name { get; set; }

    [MaxLength(StringLengthConstants.DescriptionLength)]
    public string? Description { get; set; }

    public ICollection<Guid> AttributeIds { get; set; } = new List<Guid>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var validationResult in UniqueCollectionValidator.Validate(AttributeIds, "Attribute type id"))
        {
            yield return validationResult;
        }

        var attributeRepository = (IAttributeRepository) validationContext.GetService(typeof(IAttributeRepository))!;

        foreach (var attributeId in AttributeIds)
        {
            var attribute = attributeRepository.Get(attributeId).Result;

            if (attribute == null)
            {
                yield return new ValidationResult($"Couldn't find an attribute with id {attributeId}.");
            }
        }
    }
}