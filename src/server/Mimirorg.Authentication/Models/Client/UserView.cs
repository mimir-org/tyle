namespace Mimirorg.Authentication.Models.Client;

public class UserView
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<string> Roles { get; set; } = new List<string>();
    public required string Purpose { get; set; }
}