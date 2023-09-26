namespace TypeLibrary.Api.Attributes;

public class NumericValueListConstraintView : ValueConstraintView
{
    public IEnumerable<decimal> ValueList { get; set; }
}