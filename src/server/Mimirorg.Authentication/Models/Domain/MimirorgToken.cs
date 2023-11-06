using Mimirorg.Authentication.Enums;

namespace Mimirorg.Authentication.Models.Domain;

public class MimirorgToken
{
    public int Id { get; set; }
    public required string ClientId { get; set; }
    public required string Email { get; set; }
    public required string Secret { get; set; }
    public DateTime ValidTo { get; set; }
    public TokenType TokenType { get; set; }
}