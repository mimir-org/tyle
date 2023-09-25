using System.ComponentModel.DataAnnotations;
using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Terminals.Requests;

public class TerminalTypeRequest : IValidatableObject
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public ICollection<int> ClassifierReferenceIds { get; set; }
    
    public int? PurposeReferenceId { get; set; }
    
    public string? Notation { get; set; }
    
    public string? Symbol { get; set; }
    
    public Aspect? Aspect { get; set; }

    public int? MediumReferenceId { get; set; }

    [Required]
    public Direction Qualifier { get; set; }

    [Required]
    public ICollection<AttributeTypeReferenceRequest> Attributes { get; set; }

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