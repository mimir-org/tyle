using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Abstract;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgCompanyRepository : IGenericRepository<MimirorgAuthenticationContext, MimirorgCompany>, IDynamicLogoDataProvider
    {
    }
}