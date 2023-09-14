namespace Mimirorg.TypeLibrary.Models.Domain;

public class BlockClassifierMapping
{
    public int Id { get; set; }
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public int ClassifierId { get; set; }
    public ClassifierReference Classifier { get; set; } = null!;

    public BlockClassifierMapping(Guid blockId, int classifierId)
    {
        BlockId = blockId;
        ClassifierId = classifierId;
    }
}