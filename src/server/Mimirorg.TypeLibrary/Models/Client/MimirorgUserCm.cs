using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class MimirorgUserCm
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Dictionary<int, MimirorgPermission> Permissions { get; set; }
        public ICollection<string> Roles { get; set; } = new List<string>();
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Purpose { get; set; }
    }
}