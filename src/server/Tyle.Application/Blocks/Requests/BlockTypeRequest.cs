using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Application.Blocks.Requests;

public class BlockTypeRequest : IValidatableObject
{
    [Required, MaxLength(StringLengthConstants.NameLength)]
    public required string Name { get; set; }

    [MaxLength(StringLengthConstants.DescriptionLength)]
    public string? Description { get; set; }

    public ICollection<int> ClassifierIds { get; set; } = new List<int>();

    public int? PurposeId { get; set; }

    [MaxLength(StringLengthConstants.NotationLength)]
    public string? Notation { get; set; }

    public int? SymbolId { get; set; }

    [EnumDataType(typeof(Aspect))]
    public Aspect? Aspect { get; set; }

    public ICollection<TerminalTypeReferenceRequest> Terminals { get; set; } = new List<TerminalTypeReferenceRequest>();

    public ICollection<AttributeTypeReferenceRequest> Attributes { get; set; } = new List<AttributeTypeReferenceRequest>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var validationResult in UniqueCollectionValidator.Validate(ClassifierIds, "Classifier reference id"))
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

        if (PurposeId != null)
        {
            var purposeRepository = (IPurposeRepository) validationContext.GetService(typeof(IPurposeRepository))!;

            var purpose = purposeRepository.Get((int) PurposeId).Result;

            if (purpose == null)
            {
                yield return new ValidationResult($"Couldn't find a purpose with id {PurposeId}.");
            }
        }

        if (!ClassifierIds.Any())
        {
            yield break;
        }

        var classifierRepository = (IClassifierRepository) validationContext.GetService(typeof(IClassifierRepository))!;

        foreach (var classifierId in ClassifierIds)
        {
            var classifier = classifierRepository.Get(classifierId).Result;

            if (classifier == null)
            {
                yield return new ValidationResult($"Couldn't find a classifier with id {classifierId}.");
            }
        }
    }
}