using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface INodeTypeRepository : IGenericRepository<TypeLibraryDbContext, NodeType>
    {
    }
}
