using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfTerminalAttributeRepository : GenericRepository<TypeLibraryDbContext, TerminalAttributeTypeReference>, IEfTerminalAttributeRepository
{
    public EfTerminalAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}