using Mimirorg.Authentication.Enums;

namespace Mimirorg.Authentication.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
