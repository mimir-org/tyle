using Mimirorg.TypeLibrary.Enums;
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
    /// A description of the terminal
    /// </summary>
    /// <remarks>
    /// It is allowed to change the description. Changing will generate a minor increase
    /// </remarks>
    public string Description { get; set; }

    [Required]
    public ICollection<string> Classifiers { get; set; }
    
    public string Purpose { get; set; }
    
    public string Notation { get; set; }
    
    public string Symbol { get; set; }
    
    [Required]
    public Aspect Aspect { get; set; }

    public string Medium { get; set; }

    [Required]
    public Direction Qualifier { get; set; }

    /// <summary>
    /// A list of attributes
    /// </summary>
    /// <remarks>
    /// It is not allowed to remove attributes
    /// Adding attributes generates a major increase
    /// </remarks>
    public ICollection<TerminalAttributeLibAm> TerminalAttributes { get; set; }
}