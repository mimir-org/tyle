using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models;

public class ValueConstraintLibDm
{
    public int Id { get; set; }
    public string AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; }
    public ConstraintType ConstraintType { get; set; }
    public string Value { get; set; }
    public string AllowedValues { get; set; }
    public XsdDataType DataType { get; set; }
    public int MinCount { get; set; }
    public int MaxCount { get; set; }
    public string Pattern { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public bool MinInclusive { get; set; }
    public bool MaxInclusive { get; set; }
}
