using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

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
    public async Task<int> ChangeState(State state, ICollection<string> ids)
    {
        var aspectObjectsToChange = new List<AspectObjectLibDm>();
        foreach (var id in ids)
        {
            var aspectObject = await GetAsync(id);
            aspectObject.State = state;
            aspectObjectsToChange.Add(aspectObject);
        }

        await SaveAsync();
        Detach(aspectObjectsToChange);

        return aspectObjectsToChange.Count;
    }

    /// <summary>
    /// Change all parent id's on terminals from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old terminal parent id</param>
    /// <param name="newId">New terminal parent id</param>
    /// <returns>The number of terminal with the new parent id</returns>
    public async Task<int> ChangeParentId(string oldId, string newId)
    {
        var affectedAspectObjects = FindBy(x => x.ParentId == oldId);

        foreach (var aspectObject in affectedAspectObjects)
        {
            aspectObject.ParentId = newId;
        }

        await SaveAsync();
        Detach(affectedAspectObjects.ToList());

        return affectedAspectObjects.Count();
    }

    /// <summary>
    /// Check if aspect object exists
    /// </summary>
    /// <param name="id">The id of the aspect object</param>
    /// <returns>True if aspect object exist</returns>
    public async Task<bool> Exist(string id)
    {
        return await Exist(x => x.Id == id);
    }

    /// <summary>
    /// Get all aspect objects
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    public IEnumerable<AspectObjectLibDm> Get()
    {
        return GetAll()
            .Include(x => x.AspectObjectTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.AspectObjectAttributes)
            .ThenInclude(x => x.Attribute)
            .AsSplitQuery();
    }

    /// <summary>
    /// Get aspect object by id
    /// </summary>
    /// <param name="id">The aspect object id</param>
    /// <returns>Aspect object if found</returns>
    public AspectObjectLibDm Get(string id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.AspectObjectTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.AspectObjectAttributes)
            .ThenInclude(x => x.Attribute)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /// <summary>
    /// Create an aspect object
    /// </summary>
    /// <param name="aspectObject">The aspect object to be created</param>
    /// <returns>The created aspect object</returns>
    public async Task<AspectObjectLibDm> Create(AspectObjectLibDm aspectObject)
    {
        await CreateAsync(aspectObject);
        await SaveAsync();

        Detach(aspectObject);

        return aspectObject;
    }

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}