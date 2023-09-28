using System.ComponentModel.DataAnnotations;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Attributes.Requests;

public class AttributeTypeRequest : IValidatableObject
{
    [Required, MaxLength(StringLengthConstants.NameLength)]
    public required string Name { get; set; }

    [MaxLength(StringLengthConstants.DescriptionLength)]
    public string? Description { get; set; }

    public int? PredicateId { get; set; }

    public ICollection<int> UnitIds { get; set; } = new List<int>();

    [Required, Range(0, 1, ErrorMessage = "The unit min count must be 0 or 1.")]
    public int UnitMinCount { get; set; }

    [Required, Range(0, 1, ErrorMessage = "The unit max count must be 0 or 1.")]
    public int UnitMaxCount { get; set; }

    public ProvenanceQualifier? ProvenanceQualifier { get; set; }

    public RangeQualifier? RangeQualifier { get; set; }

    public RegularityQualifier? RegularityQualifier { get; set; }

    public ScopeQualifier? ScopeQualifier { get; set; }

    public ValueConstraintRequest? ValueConstraint { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (UnitMinCount > UnitMaxCount)
        {
            yield return new ValidationResult("The unit min count cannot be larger than the unit max count.");
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(UnitIds, "Unit reference id"))
        {
            yield return validationResult;
        }
    }
}