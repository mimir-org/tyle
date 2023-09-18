using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Application.Blocks.Requests;

public class BlockTypeRequest : IValidatableObject
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

    [Required]
    public ICollection<TerminalTypeReferenceRequest> Terminals { get; }

    [Required]
    public ICollection<AttributeTypeReferenceRequest> Attributes { get; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var validationResult in UniqueCollectionValidator.Validate(ClassifierReferenceIds, "Classifier reference id"))
        {
            yield return validationResult;
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(Terminals.Select(x => (x.TerminalId, x.Direction)), "Terminal id and direction"))
        {
            yield return validationResult;
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(Attributes.Select(x => x.AttributeId), "Attribute id"))
        {
            yield return validationResult;
        }
    }
}