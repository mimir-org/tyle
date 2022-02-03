using Mimirorg.Common.Abstract;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Contracts
{
    public interface ISourceRepository : IGenericRepository<TypeLibraryDbContext, SourceLibDm>
    {
    }
}