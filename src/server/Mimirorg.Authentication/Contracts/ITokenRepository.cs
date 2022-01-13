using Mimirorg.Authentication.ApplicationModels;
using Mimirorg.Authentication.Models;
using Mimirorg.Common.Abstract;

namespace Mimirorg.Authentication.Contracts
{
    public interface ITokenRepository : IGenericRepository<AuthenticationContext, RefreshToken>
    {
        Task<TokenAm> CreateAccessToken(MimirorgUser user, DateTime current);
        Task<TokenAm> CreateRefreshToken(MimirorgUser user, DateTime current);
    }
}
