using System.ComponentModel.DataAnnotations;
using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Terminals.Requests;

public class TerminalTypeRequest : IValidatableObject
{
    [Required, MaxLength(StringLengthConstants.NameLength)]
    public required string Name { get; set; }

    [MaxLength(StringLengthConstants.DescriptionLength)]
    public string? Description { get; set; }

    public ICollection<int> ClassifierIds { get; set; } = new List<int>();

    public int? PurposeId { get; set; }

    [MaxLength(StringLengthConstants.NotationLength)]
    public string? Notation { get; set; }

    [MaxLength(StringLengthConstants.IriLength)]
    public string? Symbol { get; set; }

    public Aspect? Aspect { get; set; }

    public int? MediumId { get; set; }

    [Required]
    public Direction Qualifier { get; set; }

    public ICollection<AttributeTypeReferenceRequest> Attributes { get; set; } = new List<AttributeTypeReferenceRequest>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var validationResult in UniqueCollectionValidator.Validate(ClassifierIds, "Classifier reference id"))
        {
            yield return validationResult;
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(Attributes.Select(x => x.AttributeId), "Attribute id"))
        {
            yield return validationResult;
        }
    }
}