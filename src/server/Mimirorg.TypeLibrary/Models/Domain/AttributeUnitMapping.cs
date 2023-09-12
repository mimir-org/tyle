namespace Mimirorg.TypeLibrary.Models.Domain;

public class AttributeUnitMapping
{
    public int Id { get; set; }
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public int UnitId { get; set; }
    public UnitReference Unit { get; set; } = null!;

    public AttributeUnitMapping(Guid attributeId, int unitId)
    {
        AttributeId = attributeId;
        UnitId = unitId;
    }
}
