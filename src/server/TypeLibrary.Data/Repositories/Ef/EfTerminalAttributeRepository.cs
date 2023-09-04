using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfTerminalAttributeRepository : GenericRepository<TypeLibraryDbContext, TerminalAttributeTypeReference>, IEfTerminalAttributeRepository
{
    public EfTerminalAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}