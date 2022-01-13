using System.Security.Principal;
using Mimirorg.Authentication.ApplicationModels;

namespace Mimirorg.Authentication.Contracts
{
    public interface IUserService
    {
        Task<QrCode> CreateUser(UserAm userAm);
        Task<UserCm> GetUser(IPrincipal principal);
    }
}
