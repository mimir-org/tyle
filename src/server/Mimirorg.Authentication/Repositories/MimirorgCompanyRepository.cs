using Mimirorg.Authentication.Abstract;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Repositories;

public class MimirorgCompanyRepository : GenericRepository<MimirorgAuthenticationContext, MimirorgCompany>, IMimirorgCompanyRepository
{
    public MimirorgCompanyRepository(MimirorgAuthenticationContext dbContext) : base(dbContext)
    {
    }

    public async Task<string> GetLogoDataAsync(int id)
    {
        var company = await GetAsync(id);
        return company?.Logo;
    }
}