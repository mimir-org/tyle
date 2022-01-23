using Mimirorg.Authentication.Models.Content;
using Mimirorg.Authentication.Models.Enums;

namespace Mimirorg.Authentication.Models.Domain
{
    public class MimirorgToken
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }
        public DateTime ValidTo { get; set; }
        public MimirorgTokenType TokenType { get; set; }

        public MimirorgTokenCm ToContentModel()
        {
            return new MimirorgTokenCm
            {
                ClientId = ClientId,
                Secret = Secret,
                TokenType = TokenType,
                ValidTo = ValidTo
            };
        }
    }
}
