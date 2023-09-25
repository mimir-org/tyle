using System.ComponentModel.DataAnnotations;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Attributes.Requests;

public class AttributeTypeRequest : IValidatableObject
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public int? PredicateReferenceId { get; set; }

    [Required]
    public ICollection<int> UnitReferenceIds { get; set; }

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

        foreach (var validationResult in UniqueCollectionValidator.Validate(UnitReferenceIds, "Unit reference id"))
        {
            yield return validationResult;
        }
    }
}