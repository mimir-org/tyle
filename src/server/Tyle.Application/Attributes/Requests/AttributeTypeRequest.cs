using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Attributes;

namespace Tyle.Application.Attributes.Requests;

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

    [EnumDataType(typeof(ProvenanceQualifier))]
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }

    [EnumDataType(typeof(RangeQualifier))]
    public RangeQualifier? RangeQualifier { get; set; }

    [EnumDataType(typeof(RegularityQualifier))]
    public RegularityQualifier? RegularityQualifier { get; set; }

    [EnumDataType(typeof(ScopeQualifier))]
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