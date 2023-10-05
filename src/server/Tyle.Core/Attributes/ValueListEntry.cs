namespace Tyle.Core.Attributes;

public class ValueListEntry
{
    public int Id { get; set; }
    public Guid ValueConstraintId { get; set; }
    public ValueConstraint ValueConstraint { get; set; } = null!;
    public required string EntryValue { get; set; }
}