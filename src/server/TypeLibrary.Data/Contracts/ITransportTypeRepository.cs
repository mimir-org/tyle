using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface ITransportTypeRepository : IGenericRepository<TypeLibraryDbContext, TransportType>
    {
    }
}
