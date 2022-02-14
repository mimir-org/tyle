using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IInterfaceRepository : IGenericRepository<TypeLibraryDbContext, InterfaceLibDm>
    {
    }
}
