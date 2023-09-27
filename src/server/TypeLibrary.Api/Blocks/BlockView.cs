using TypeLibrary.Core.Blocks;
using TypeLibrary.Core.Common;

namespace TypeLibrary.Api.Blocks;

public class BlockView : ImfType
{
    public ICollection<RdlClassifier> Classifiers { get; set; }
    public RdlPurpose? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public ICollection<BlockTerminalTypeReference> Terminals { get; set; }
    public ICollection<BlockAttributeTypeReference> Attributes { get; set; }
}