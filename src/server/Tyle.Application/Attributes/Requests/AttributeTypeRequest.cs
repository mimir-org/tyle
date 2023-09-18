using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common.Requests;
using Tyle.Core.Attributes;

namespace Tyle.Application.Attributes.Requests;

public class AttributeTypeRequest : IValidatableObject
{
    [Required]
    public string Name { get; }

    public string? Description { get; }

    public int? PredicateReferenceId { get; }

    [Required]
    public ICollection<int> UnitReferenceIds { get; }

    [Required, Range(0, 1, ErrorMessage = "The unit min count must be 0 or 1.")]
    public int UnitMinCount { get; }

    [Required, Range(0, 1, ErrorMessage = "The unit max count must be 0 or 1.")]
    public int UnitMaxCount { get; }

    public ProvenanceQualifier? ProvenanceQualifier { get; }

    public RangeQualifier? RangeQualifier { get; }

    public RegularityQualifier? RegularityQualifier { get; }

    public ScopeQualifier? ScopeQualifier { get; }
    
    public ValueConstraintRequest? ValueConstraint { get; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (UnitMinCount > UnitMaxCount)
        {
            yield return new ValidationResult("The unit min count cannot be larger than the unit max count.");
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(UnitReferenceIds, "Unit reference id"))
        {
            yield return validationResult;
        }
    }
}