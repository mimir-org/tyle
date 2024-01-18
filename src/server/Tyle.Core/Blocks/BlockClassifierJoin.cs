using Tyle.Core.Common;

namespace Tyle.Core.Blocks;

public class BlockClassifierJoin
{
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public int ClassifierId { get; set; }
    public RdlClassifier Classifier { get; set; } = null!;
}