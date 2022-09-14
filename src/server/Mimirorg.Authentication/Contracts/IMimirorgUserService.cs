using System.Security.Principal;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgUserService
    {
        Task<MimirorgQrCodeCm> CreateUser(MimirorgUserAm userAm);
        Task<MimirorgUserCm> GetUser(IPrincipal principal);
        Task<MimirorgUserCm> GetUser(string id);
        Task<ICollection<MimirorgCompanyCm>> GetUserFilteredCompanies();
        Task<MimirorgUserCm> UpdateUser(string id, MimirorgUserAm userAm);
        Task<bool> DeleteUser(string id);
    }
}