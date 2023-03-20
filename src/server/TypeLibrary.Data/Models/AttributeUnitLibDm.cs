namespace TypeLibrary.Data.Models
{
    public class AttributeUnitLibDm
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public AttributeLibDm Attribute { get; set; }
        public int UnitId { get; set; }
        public UnitLibDm Unit { get; set; }
        public bool IsDefault { get; set; }
    }
}