using System.ComponentModel.DataAnnotations;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Attributes.Requests;

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
