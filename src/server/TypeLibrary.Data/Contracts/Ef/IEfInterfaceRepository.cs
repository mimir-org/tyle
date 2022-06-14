using System.Linq;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfInterfaceRepository : IGenericRepository<TypeLibraryDbContext, InterfaceLibDm>
    {
        IQueryable<InterfaceLibDm> GetAllInterfaces();
        IQueryable<InterfaceLibDm> FindInterface(string id);
    }
}
