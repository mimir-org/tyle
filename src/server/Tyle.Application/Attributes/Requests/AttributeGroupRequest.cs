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
        return UniqueCollectionValidator.Validate(AttributeIds, "Attribute type id");
    }
}