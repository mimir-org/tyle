using Microsoft.AspNetCore.Identity;

namespace Mimirorg.Authentication.Models
{
    public class MimirorgUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
