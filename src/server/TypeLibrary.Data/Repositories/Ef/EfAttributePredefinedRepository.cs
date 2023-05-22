using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAttributePredefinedRepository : GenericRepository<TypeLibraryDbContext, AttributePredefinedLibDm>, IEfAttributePredefinedRepository
{
    public EfAttributePredefinedRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
    public IEnumerable<AttributePredefinedLibDm> GetPredefined()
    {
        return GetAll();
    }

    /// <inheritdoc />
    public async Task<AttributePredefinedLibDm> CreatePredefined(AttributePredefinedLibDm predefined)
    {
        await CreateAsync(predefined);
        await SaveAsync();
        return predefined;
    }
}