namespace Tyle.Api.Attributes;

public class NumericValueListConstraintView : ValueConstraintView
{
    public required IEnumerable<decimal> ValueList { get; set; }
}