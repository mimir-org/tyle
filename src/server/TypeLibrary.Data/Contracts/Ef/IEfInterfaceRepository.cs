using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfInterfaceRepository : IGenericRepository<TypeLibraryDbContext, InterfaceLibDm>
    {
        Task<InterfaceLibDm> Get(string id);
        IQueryable<InterfaceLibDm> GetAllInterfaces();
        IQueryable<InterfaceLibDm> FindInterface(string id);
    }
}