using Tyle.Core.Common;

namespace Tyle.Core.Blocks;

public class BlockType : ImfType
{
    public ICollection<ClassifierReference> Classifiers { get; }
    public PurposeReference? Purpose { get; }
    public string? Notation { get; }
    public string? Symbol { get; }
    public Aspect? Aspect { get; }

    public ICollection<TerminalTypeReference> BlockTerminals { get; set; }
    public ICollection<AttributeTypeReference> Attributes { get; set; }

    /// <summary>
    /// Creates a new block type
    /// </summary>
    /// <param name="name">The name of the block type.</param>
    /// <param name="description">A description of the block type. Can be null.</param>
    /// <param name="createdBy">A user struct containing information about the user creating the type.</param>
    public BlockType(string name, string? description, User createdBy) : base(name, description, createdBy)
    {
        Classifiers = new List<ClassifierReference>();
        BlockTerminals = new List<TerminalTypeReference>();
        Attributes = new List<AttributeTypeReference>();
    }
}