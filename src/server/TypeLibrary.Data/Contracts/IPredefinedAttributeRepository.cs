using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Contracts
{
    public interface IPredefinedAttributeRepository : IGenericRepository<TypeLibraryDbContext, PredefinedAttribute>
    {
    }
}
