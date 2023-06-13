using Microsoft.AspNetCore.Identity;

namespace Mimirorg.Authentication.Models.Domain;

public class MimirorgUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SecurityHash { get; set; }
    public List<MimirorgCompany> ManageCompanies { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Purpose { get; set; }
}