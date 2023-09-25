namespace TypeLibrary.Core.Common;

public abstract class ImfType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Version { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<string> ContributedBy { get; set; } = new List<string>();
    public DateTimeOffset LastUpdateOn { get; set; }
}
