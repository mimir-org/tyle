using System.Security.Principal;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Content;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgUserService
    {
        Task<MimirorgQrCodeCm> CreateUser(MimirorgUserAm userAm);
        Task<MimirorgUserCm> GetUser(IPrincipal principal);
        Task<MimirorgUserCm> GetUser(string id);
        Task<MimirorgUserCm> UpdateUser(string id, MimirorgUserAm userAm);
        Task<bool> DeleteUser(string id);
    }
}
