using Mimirorg.Common.Abstract;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Contracts
{
    public interface INodeTypeRepository : IGenericRepository<TypeLibraryDbContext, NodeDm>
    {
    }
}
