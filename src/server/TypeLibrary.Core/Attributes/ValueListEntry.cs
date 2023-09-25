namespace TypeLibrary.Core.Attributes;

public class ValueListEntry
{
    public int Id { get; set; }
    public Guid ValueConstraintId { get; set; }
    public ValueConstraint ValueConstraint { get; set; }
    public string EntryValue { get; set; }
}