namespace TypeLibrary.Core.Common;

public abstract class ImfType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Version { get; set; }
    public DateTimeOffset CreatedOn { get; init; }
    public required string CreatedBy { get; init; }
    public ICollection<string> ContributedBy { get; } = new HashSet<string>();
    public DateTimeOffset LastUpdateOn { get; set; }
}