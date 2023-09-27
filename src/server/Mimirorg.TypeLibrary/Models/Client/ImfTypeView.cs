namespace Mimirorg.TypeLibrary.Models.Client;

public abstract class ImfTypeView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<string> ContributedBy { get; set; }
    public DateTimeOffset LastUpdateOn { get; set; }
}