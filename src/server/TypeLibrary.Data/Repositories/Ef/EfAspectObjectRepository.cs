using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAspectObjectRepository : GenericRepository<TypeLibraryDbContext, AspectObjectLibDm>, IEfAspectObjectRepository
{
    public EfAspectObjectRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
    public int HasCompany(string id)
    {
        return Get(id).CompanyId;
    }

    /// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var aspectObject = await GetAsync(id);
        aspectObject.State = state;
        await SaveAsync();
        Detach(aspectObject);
    }

    /// <inheritdoc />
    public IEnumerable<AspectObjectLibDm> Get()
    {
        return GetAll()
            .Include(x => x.AspectObjectTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.AspectObjectAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Rds)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public AspectObjectLibDm Get(string id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.AspectObjectTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.AspectObjectAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Rds)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public IEnumerable<AspectObjectLibDm> GetAllVersions(AspectObjectLibDm aspectObject)
    {
        return FindBy(x => x.FirstVersionId == aspectObject.FirstVersionId)
            .Include(x => x.AspectObjectTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.AspectObjectAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Rds)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public async Task<AspectObjectLibDm> Create(AspectObjectLibDm aspectObject)
    {
        await CreateAsync(aspectObject);
        await SaveAsync();

        Detach(aspectObject);

        return aspectObject;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}