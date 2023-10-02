namespace TypeLibrary.Api.Attributes;

public class AttributeGroupView
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedOn { get; init; }
    public required string CreatedBy { get; init; }
    public ICollection<string> ContributedBy { get; } = new HashSet<string>();
    public DateTimeOffset LastUpdateOn { get; set; }
    public ICollection<AttributeView> Attributes { get; set; } = new List<AttributeView>();
}