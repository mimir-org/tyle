using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Repositories.Common;

public class TypeLibraryProcRepository : ProcRepository<TypeLibraryDbContext>, ITypeLibraryProcRepository
{
    public TypeLibraryProcRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}