using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Abstract;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgHookRepository : GenericRepository<MimirorgAuthenticationContext, MimirorgHook>, IMimirorgHookRepository
    {
        public MimirorgHookRepository(MimirorgAuthenticationContext dbContext) : base(dbContext)
        {
        }
    }
}