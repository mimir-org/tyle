using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Application.Terminals.Requests;

public class TerminalTypeRequest : IValidatableObject
{
    [Required]
    public string Name { get; }

    public string? Description { get; }

    [Required]
    public ICollection<int> ClassifierReferenceIds { get; }
    
    public int? PurposeReferenceId { get; }
    
    public string? Notation { get; }
    
    public string? Symbol { get; }
    
    public Aspect? Aspect { get; }

    public int? MediumReferenceId { get; }

    [Required]
    public Direction Qualifier { get; }

    [Required]
    public ICollection<AttributeTypeReferenceRequest> Attributes { get; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var validationResult in UniqueCollectionValidator.Validate(ClassifierReferenceIds, "Classifier reference id"))
        {
            yield return validationResult;
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(Attributes.Select(x => x.AttributeId), "Attribute id"))
        {
            yield return validationResult;
        }
    }
}