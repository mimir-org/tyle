using Mimirorg.Authentication.Abstract;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgTokenRepository : IGenericRepository<MimirorgAuthenticationContext, MimirorgToken>
{
    Task<MimirorgTokenCm> CreateAccessToken(MimirorgUser user, DateTime current);
    Task<MimirorgTokenCm> CreateRefreshToken(MimirorgUser user, DateTime current);
}