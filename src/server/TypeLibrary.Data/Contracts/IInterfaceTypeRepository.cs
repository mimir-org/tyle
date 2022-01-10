using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface IInterfaceTypeRepository : IGenericRepository<TypeLibraryDbContext, InterfaceType>
    {
    }
}
