namespace Mimirorg.Authentication.Models.Domain
{
    public class MimirorgRefreshToken
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
