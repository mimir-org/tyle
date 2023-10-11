namespace Tyle.Core.Attributes;

public class ValueConstraint
{
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public ConstraintType ConstraintType { get; set; }
    public XsdDataType DataType { get; set; }
    public int? MinCount { get; set; }
    public int? MaxCount { get; set; }
    public string? Value { get; set; }
    public ICollection<ValueListEntry> ValueList { get; set; } = new List<ValueListEntry>();
    public string? Pattern { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
}