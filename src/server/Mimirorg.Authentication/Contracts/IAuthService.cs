using Mimirorg.Authentication.ApplicationModels;
using Mimirorg.Authentication.Models;

namespace Mimirorg.Authentication.Contracts
{
    public interface IAuthService
    {
        Task<ICollection<TokenAm>> Authenticate(AuthenticateAm authenticate);
        Task<ICollection<TokenAm>> Authenticate(string secret);
        Task LockUser(MimirorgUser user);
    }
}
