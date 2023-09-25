using TypeLibrary.Core.Common;

namespace TypeLibrary.Core.Blocks;

public class BlockClassifierJoin
{
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; }
    public int ClassifierId { get; set; }
    public RdlClassifier Classifier { get; set; }
}