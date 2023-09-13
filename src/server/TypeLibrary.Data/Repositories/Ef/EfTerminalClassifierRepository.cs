using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfTerminalClassifierRepository : GenericRepository<TypeLibraryDbContext, TerminalClassifierMapping>, IEfTerminalClassifierRepository
{
    public EfTerminalClassifierRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}