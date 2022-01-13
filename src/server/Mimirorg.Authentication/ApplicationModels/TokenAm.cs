using Mimirorg.Authentication.Enums;

namespace Mimirorg.Authentication.ApplicationModels
{
    public class TokenAm
    {
        public string ClientId { get; set; }
        public TokenType TokenType { get; set; }
        public string Secret { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
