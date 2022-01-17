using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Contracts
{
    public interface IBlobDataRepository : IGenericRepository<TypeLibraryDbContext, BlobData>
    {
    }
}
