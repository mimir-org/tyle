using Mimirorg.Authentication.Abstract;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgHookRepository : IGenericRepository<MimirorgAuthenticationContext, MimirorgHook>
{
}