using Mimirorg.Authentication.Models.Enums;

namespace Mimirorg.Authentication.Models.Content
{
    public class MimirorgTokenCm
    {
        public string ClientId { get; set; }
        public MimirorgTokenType TokenType { get; set; }
        public string Secret { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
