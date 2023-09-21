using Tyle.Core.Common;

namespace Tyle.Core.Terminals;

public class TerminalType : ImfType
{
    public ICollection<ClassifierReference> Classifiers { get; set; }
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public MediumReference? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<AttributeTypeReference> Attributes { get; set; }

    /// <summary>
    /// Creates a new terminal type
    /// </summary>
    /// <param name="name">The name of the terminal type.</param>
    /// <param name="description">A description of the terminal type. Can be null.</param>
    /// <param name="createdBy">A user struct containing information about the user creating the type.</param>
    public TerminalType(string name, string? description, User createdBy) : base(name, description, createdBy)
    {
        Qualifier = Direction.Bidirectional;
        Classifiers = new List<ClassifierReference>();
        Attributes = new List<AttributeTypeReference>();
    }
}