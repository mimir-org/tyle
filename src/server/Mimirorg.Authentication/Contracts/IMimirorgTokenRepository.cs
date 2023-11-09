using Mimirorg.Authentication.Abstract;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgTokenRepository : IGenericRepository<MimirorgAuthenticationContext, MimirorgToken>
{
    Task<TokenView> CreateAccessToken(MimirorgUser user, DateTime current);
    Task<TokenView> CreateRefreshToken(MimirorgUser user, DateTime current);
}