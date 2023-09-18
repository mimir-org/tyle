namespace Tyle.Core.Common;

public abstract class ImfType
{
    public Guid Id { get; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Version { get; }
    public DateTimeOffset CreatedOn { get; }
    public User CreatedBy { get; }
    public ICollection<User> ContributedBy { get; }
    public DateTimeOffset LastUpdateOn { get; set; }

    protected ImfType(string name, string? description, User createdBy)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Version = "1.0";
        CreatedOn = DateTimeOffset.Now;
        CreatedBy = createdBy;
        ContributedBy = new HashSet<User>();
        LastUpdateOn = CreatedOn;
    }
}
