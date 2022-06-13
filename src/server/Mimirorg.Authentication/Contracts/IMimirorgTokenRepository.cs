using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgTokenRepository : IGenericRepository<MimirorgAuthenticationContext, MimirorgToken>
    {
        Task<MimirorgTokenCm> CreateAccessToken(MimirorgUser user, DateTime current);
        Task<MimirorgTokenCm> CreateRefreshToken(MimirorgUser user, DateTime current);
    }
}
