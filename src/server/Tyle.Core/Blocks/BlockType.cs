using Tyle.Core.Common;

namespace Tyle.Core.Blocks;

public class BlockType : ImfType
{
    public ICollection<BlockClassifierJoin> Classifiers { get; set; } = new List<BlockClassifierJoin>();
    public int? PurposeId { get; set; }
    public RdlPurpose? Purpose { get; set; }
    public string? Notation { get; set; }
    public int? SymbolId { get; set; }
    public EngineeringSymbol? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public ICollection<BlockTerminalTypeReference> Terminals { get; set; } = new List<BlockTerminalTypeReference>();
    public ICollection<BlockAttributeTypeReference> Attributes { get; set; } = new List<BlockAttributeTypeReference>();
}