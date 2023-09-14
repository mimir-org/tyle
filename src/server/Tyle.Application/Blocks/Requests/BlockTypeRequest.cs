using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common.Validation;
using Tyle.Core.Common.Domain;

namespace Tyle.Application.Blocks.Requests;

/// <summary>
/// Object used to create or update a block
/// </summary>
public class BlockTypeRequest : IValidatableObject // : ICompanyObject
{
    /// <summary>
    /// The name of the block
    /// </summary>
    /// <remarks>
    /// The name is not allowed to change
    /// </remarks>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// The description of the block type
    /// </summary>
    /// <remarks>
    /// A description change, will trigger a minor version increase
    /// </remarks>
    public string? Description { get; set; }

    /*/// <summary>
    /// The block version
    /// </summary>
    [Required]
    [Double]
    public string Version { get; set; }

    /// <summary>
    /// The owner of the block type
    /// </summary>
    /// <remarks>
    /// A company id change, will trigger a minor version increase
    /// </remarks>
    [Required]
    [Display(Name = "CompanyId")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
    public int CompanyId { get; set; }*/

    [Required]
    public ICollection<int> ClassifierReferenceIds { get; set; }

    /// <summary>
    /// The purpose name of the block
    /// </summary>
    /// <remarks>
    /// A purpose name change will trigger a minor version increase
    /// </remarks>
    public int? PurposeReferenceId { get; set; }

    /// <summary>
    /// The id of the RDS of the block
    /// </summary>
    /// <remarks>
    /// The RDS is not allowed to change
    /// </remarks>
    public string? Notation { get; set; }

    /// <summary>
    /// The symbol of the block type
    /// </summary>
    /// <remarks>
    /// A symbol change, will trigger a minor version increase
    /// </remarks>
    public string? Symbol { get; set; }

    /// <summary>
    /// The aspect of the block
    /// </summary>
    /// <remarks>
    /// The aspect is not allowed to change
    /// </remarks>
    [Required]
    public Aspect Aspect { get; set; }

    /// <summary>
    /// A list of connected terminals
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove terminals
    /// Adding terminals generates a major increase
    /// </remarks>
    public ICollection<BlockTerminalRequest> BlockTerminals { get; set; }

    /// <summary>
    /// A list of attributes
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove attributes
    /// Adding attributes generates a major increase
    /// </remarks>
    public ICollection<BlockAttributeRequest> BlockAttributes { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var validationResult in UniqueCollectionValidator.Validate(ClassifierReferenceIds,
                     "Classifier reference id"))
        {
            yield return validationResult;
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(
                     BlockTerminals.Select(x => (x.TerminalId, x.Direction)), "Terminal id and direction"))
        {
            yield return validationResult;
        }

        foreach (var validationResult in UniqueCollectionValidator.Validate(
                     BlockAttributes.Select(x => x.AttributeId), "Attribute id"))
        {
            yield return validationResult;
        }
    }
}