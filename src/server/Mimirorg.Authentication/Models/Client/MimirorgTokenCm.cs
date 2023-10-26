using Mimirorg.Authentication.Enums;

namespace Mimirorg.Authentication.Models.Client;

public class MimirorgTokenCm
{
    public string ClientId { get; set; }
    public MimirorgTokenType TokenType { get; set; }
    public string Secret { get; set; }
    public DateTime ValidTo { get; set; }
}