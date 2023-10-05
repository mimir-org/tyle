namespace Tyle.Core.Attributes;

public class AttributeUnitJoin
{
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public int UnitId { get; set; }
    public RdlUnit Unit { get; set; } = null!;
}