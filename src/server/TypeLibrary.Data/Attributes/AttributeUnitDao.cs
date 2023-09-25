using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyle.Persistence.Attributes;

[Table("Attribute_Unit")]
[PrimaryKey(nameof(AttributeId), nameof(UnitId))]
public class AttributeUnitDao
{
    public Guid AttributeId { get; set; }
    public AttributeDao Attribute { get; set; } = null!;

    public int UnitId { get; set; }
    public UnitDao Unit { get; set; } = null!;

    public AttributeUnitDao(Guid attributeId, int unitId)
    {
        AttributeId = attributeId;
        UnitId = unitId;
    }
}