using Tyle.Core.Common;

namespace Tyle.Core.Blocks;

public class BlockType : ImfType
{
    public ICollection<BlockClassifierMapping> Classifiers { get; set; }
    public int? PurposeId { get; set; }
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect Aspect { get; set; }

    public ICollection<BlockTerminalTypeReference> BlockTerminals { get; set; }
    public ICollection<BlockAttributeTypeReference> BlockAttributes { get; set; }

    public BlockType(string name, string? description, string createdBy) : base(name, description, createdBy)
    {
        Classifiers = new List<BlockClassifierMapping>();
        BlockTerminals = new List<BlockTerminalTypeReference>();
        BlockAttributes = new List<BlockAttributeTypeReference>();
    }
}