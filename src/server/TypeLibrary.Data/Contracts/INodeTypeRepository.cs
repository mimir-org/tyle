using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface INodeTypeRepository : IGenericRepository<TypeLibraryDbContext, NodeType>
    {
    }
}
