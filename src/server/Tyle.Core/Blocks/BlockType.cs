using Tyle.Core.Common;

namespace Tyle.Core.Blocks;

public class BlockType : ImfType
{
    public ICollection<ClassifierReference> Classifiers { get; set; }
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }

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