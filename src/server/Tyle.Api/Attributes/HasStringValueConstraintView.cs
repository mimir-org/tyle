namespace Tyle.Api.Attributes;

public class HasStringValueConstraintView : ValueConstraintView
{
    public required string Value { get; set; }
}