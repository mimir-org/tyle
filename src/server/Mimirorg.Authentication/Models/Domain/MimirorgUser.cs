using Microsoft.AspNetCore.Identity;

namespace Mimirorg.Authentication.Models.Domain;

public class MimirorgUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? SecurityHash { get; set; }
    public required string Purpose { get; set; }
}