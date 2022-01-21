using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Abstract;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgCompanyRepository : GenericRepository<MimirorgAuthenticationContext, MimirorgCompany>, IMimirorgCompanyRepository
    {
        public MimirorgCompanyRepository(MimirorgAuthenticationContext dbContext) : base(dbContext)
        {
        }
    }
}
