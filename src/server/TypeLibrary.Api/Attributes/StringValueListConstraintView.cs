namespace TypeLibrary.Api.Attributes;

public class StringValueListConstraintView : ValueConstraintView
{
    public IEnumerable<string> ValueList { get; set; }
}