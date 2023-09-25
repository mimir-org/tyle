using System.ComponentModel.DataAnnotations;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Blocks.Requests;

public class BlockTypeRequest : IValidatableObject
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

    [Required]
    public ICollection<TerminalTypeReferenceRequest> Terminals { get; set; }

    [Required]
    public ICollection<AttributeTypeReferenceRequest> Attributes { get; set; }

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