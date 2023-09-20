namespace Tyle.Core.Common;

public abstract class ImfType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Version { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public User CreatedBy { get; set; }
    public ICollection<User> ContributedBy { get; set; }
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
