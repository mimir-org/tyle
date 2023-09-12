using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfLogRepository : GenericRepository<TypeLibraryDbContext, LogLibDm>, IEfLogRepository
{
    public EfLogRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
    public IEnumerable<LogLibDm> Get()
    {
        return GetAll();
    }

    public IEnumerable<LogLibDm> Get(string objectId)
    {
        return FindBy(x => x.ObjectId == objectId);
    }

    /// <inheritdoc />
    public async Task Create(ICollection<LogLibDm> logDms)
    {
        foreach (var dm in logDms)
            await CreateAsync(dm);

        await SaveAsync();
    }
}