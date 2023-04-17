using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Attributes;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

/// <summary>
/// Object used to create or update a aspect object
/// </summary>
public class AspectObjectLibAm
{
    /// <summary>
    /// The name of the aspect object
    /// </summary>
    /// <remarks>
    /// The name is not allowed to change
    /// </remarks>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// A list of references to other ontologies
    /// </summary>
    /// <remarks>
    /// It is allowed to change the list. Changing will generate a minor increase
    /// </remarks>
    public string TypeReference { get; set; }

    /// <summary>
    /// The aspect object version
    /// </summary>
    [Required]
    [Double]
    public string Version { get; set; }

    /// <summary>
    /// The owner of the aspect object type
    /// </summary>
    /// <remarks>
    /// A company id change, will trigger a minor version increase
    /// </remarks>
    [Display(Name = "CompanyId")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
    public int CompanyId { get; set; }

    /// <summary>
    /// The aspect of the aspect object
    /// </summary>
    /// <remarks>
    /// The aspect is not allowed to change
    /// </remarks>
    [Required]
    public Aspect Aspect { get; set; }

    /// <summary>
    /// The purpose name of the aspect object
    /// </summary>
    /// <remarks>
    /// A purpose name change will trigger a minor version increase
    /// </remarks>
    [Required]
    public string PurposeName { get; set; }

    /// <summary>
    /// The id of the RDS of the aspect object
    /// </summary>
    /// <remarks>
    /// The RDS is not allowed to change
    /// </remarks>
    [Required]
    public string RdsId { get; set; }

    /// <summary>
    /// The symbol of the aspect object type
    /// </summary>
    /// <remarks>
    /// A symbol change, will trigger a minor version increase
    /// </remarks>
    public string Symbol { get; set; }

    /// <summary>
    /// The description of the aspect object type
    /// </summary>
    /// <remarks>
    /// A description change, will trigger a minor version increase
    /// </remarks>
    public string Description { get; set; }

    /// <summary>
    /// The parent id for the aspect object type
    /// </summary>
    /// <remarks>
    /// The parent id is not allowed to change
    /// </remarks>
    public string ParentId { get; set; }

    /// <summary>
    /// A list of connected terminals
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove terminals
    /// Adding terminals generates a major increase
    /// </remarks>
    public ICollection<AspectObjectTerminalLibAm> AspectObjectTerminals { get; set; }

    /// <summary>
    /// A list of attribute ids
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove attributes
    /// Adding attributes generates a major increase
    /// </remarks>
    public ICollection<string> Attributes { get; set; }

    /// <summary>
    /// A list of selected predefined attributes
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove predefined attributes
    /// Adding predefined attributes generates a major increase
    /// </remarks>
    public ICollection<SelectedAttributePredefinedLibAm> SelectedAttributePredefined { get; set; }
}