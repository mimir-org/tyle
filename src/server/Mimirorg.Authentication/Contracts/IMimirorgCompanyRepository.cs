using Mimirorg.Authentication.Abstract;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgCompanyRepository : IGenericRepository<MimirorgAuthenticationContext, MimirorgCompany>, IDynamicLogoDataProvider
{
}