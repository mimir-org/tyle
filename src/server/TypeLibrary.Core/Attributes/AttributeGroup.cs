namespace TypeLibrary.Core.Attributes;

public class AttributeGroup
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedOn { get; init; }
    public required string CreatedBy { get; init; }
    public ICollection<string> ContributedBy { get; } = new List<string>();
    public DateTimeOffset LastUpdateOn { get; set; }
    public ICollection<AttributeGroupAttributeJoin> Attributes { get; set; } = new List<AttributeGroupAttributeJoin>();
}
