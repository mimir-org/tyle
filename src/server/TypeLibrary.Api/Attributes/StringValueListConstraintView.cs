namespace TypeLibrary.Api.Attributes;

public class StringValueListConstraintView : ValueConstraintView
{
    public required IEnumerable<string> ValueList { get; set; }
}