using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface INodeTypeRepository : IGenericRepository<TypeLibraryDbContext, NodeType>
    {
    }
}
