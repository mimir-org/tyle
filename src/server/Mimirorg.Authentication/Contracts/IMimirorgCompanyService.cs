using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Content;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgCompanyService
    {
        Task<MimirorgCompanyCm> CreateCompany(MimirorgCompanyAm company);
        Task<IEnumerable<MimirorgCompanyCm>> GetAllCompanies();
        Task<MimirorgCompanyCm> GetCompanyById(int id);
        Task<MimirorgCompanyCm> UpdateCompany(int id, MimirorgCompanyAm company);
        Task<bool> DeleteCompany(int id);
    }
}
