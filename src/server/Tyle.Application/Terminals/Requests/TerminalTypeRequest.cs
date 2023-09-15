using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common.Validation;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

// ReSharper disable InconsistentNaming

namespace Tyle.Application.Terminals.Requests;

public class TerminalTypeRequest : IValidatableObject
{
    /// <summary>
    /// The name of the terminal
    /// </summary>
    /// <remarks>
    /// The name is not allowed to change
    /// </remarks>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// A description of the terminal
    /// </summary>
    /// <remarks>
    /// It is allowed to change the description. Changing will generate a minor increase
    /// </remarks>
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

    /// <summary>
    /// A list of attributes
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove attributes
    /// Adding attributes generates a major increase
    /// </remarks>
    [Required]
    public ICollection<TerminalAttributeRequest> TerminalAttributes { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var validationResult in UniqueCollectionValidator.Validate(ClassifierReferenceIds,
                     "Classifier reference id"))
        {
            yield return validationResult;
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(
                     TerminalAttributes.Select(x => x.AttributeId), "Attribute id"))
        {
            yield return validationResult;
        }
    }
}