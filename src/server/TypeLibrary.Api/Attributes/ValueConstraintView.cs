using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Api.Attributes;

public class ValueConstraintView
{
    public ConstraintType ConstraintType { get; set; }
    public XsdDataType DataType { get; set; }
    public int? MinCount { get; set; }
    public int? MaxCount { get; set; }
    public string? Pattern { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public bool? MinInclusive { get; set; }
    public bool? MaxInclusive { get; set; }

}