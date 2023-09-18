using Tyle.Core.Common;

namespace Tyle.Core.Terminals;

public class TerminalType : ImfType
{
    public ICollection<ClassifierReference> Classifiers { get; }
    public PurposeReference? Purpose { get; }
    public string? Notation { get; }
    public string? Symbol { get; }
    public Aspect? Aspect { get; }
    public MediumReference? Medium { get; }
    public Direction Qualifier { get; }
    public ICollection<AttributeTypeReference> Attributes { get; }

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