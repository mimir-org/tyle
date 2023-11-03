using Mimirorg.Authentication.Enums;

namespace Mimirorg.Authentication.Models.Client;

public class TokenView
{
    public required string ClientId { get; set; }
    public MimirorgTokenType TokenType { get; set; }
    public required string Secret { get; set; }
    public DateTime ValidTo { get; set; }
}