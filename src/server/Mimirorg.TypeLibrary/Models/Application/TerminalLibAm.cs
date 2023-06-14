using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace Mimirorg.TypeLibrary.Models.Application;

public class TerminalLibAm
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
    /// A list of references to other ontologies
    /// </summary>
    /// <remarks>
    /// It is allowed to change the list. Changing will generate a minor increase
    /// </remarks>
    public string TypeReference { get; set; }

    /// <summary>
    /// The color of the terminal
    /// </summary>
    /// <remarks>
    /// It is allowed to change the color. Changing will generate a minor increase
    /// </remarks>
    [Required]
    public string Color { get; set; }

    /// <summary>
    /// A description of the terminal
    /// </summary>
    /// <remarks>
    /// It is allowed to change the description. Changing will generate a minor increase
    /// </remarks>
    public string Description { get; set; }

    /// <summary>
    /// A list of attribute ids
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove attributes
    /// Adding attributes generates a major increase
    /// </remarks>
    public ICollection<string> Attributes { get; set; }
}