using Mb.Models.Abstract;
using Mb.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeTypeRepository : IGenericRepository<TypeLibraryDbContext, AttributeType>
    {
    }
}
