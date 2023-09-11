using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class AttributeLibAm : IValidatableObject
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public int? PredicateReferenceId { get; set; }

    [Required]
    public ICollection<int> UnitReferenceIds { get; set; }
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }

    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    
    public ValueConstraintLibAm ValueConstraint { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (UnitMinCount != 0 && UnitMinCount != 1)
        {
            yield return new ValidationResult("The unit min count must be 0 or 1.");
        }

        if (UnitMaxCount != 0 && UnitMaxCount != 1)
        {
            yield return new ValidationResult("The unit max count must be 0 or 1.");
        }

        if (UnitMinCount > UnitMaxCount)
        {
            yield return new ValidationResult("The unit min count cannot be larger than the unit max count.");
        }

        if (UnitMaxCount == 0 && !UnitReferenceIds.IsNullOrEmpty())
        {
            yield return new ValidationResult("Unit max count is 0, but the unit list is not empty.");
        }
    }
}