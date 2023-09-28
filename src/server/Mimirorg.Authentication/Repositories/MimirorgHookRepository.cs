using Mimirorg.Authentication.Abstract;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Repositories;

public class MimirorgHookRepository : GenericRepository<MimirorgAuthenticationContext, MimirorgHook>, IMimirorgHookRepository
{
    public MimirorgHookRepository(MimirorgAuthenticationContext dbContext) : base(dbContext)
    {
    }
}