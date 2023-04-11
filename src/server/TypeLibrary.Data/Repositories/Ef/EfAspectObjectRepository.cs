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
    private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

    public EfAspectObjectRepository(TypeLibraryDbContext dbContext, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
    {
        _typeLibraryProcRepository = typeLibraryProcRepository;
    }

    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The aspect object id</param>
    /// <returns>The company id of given terminal</returns>
    public async Task<int> HasCompany(string id)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "AspectObject"},
            {"@Id", id}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
        return result?.FirstOrDefault()?.CompanyId ?? 0;
    }

    /// <summary>
    /// Change the state of the aspect object on all listed id's
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of aspect object id's</param>
    /// <returns>The number of aspect objects in given state</returns>
    public async Task<int> ChangeState(State state, ICollection<string> ids)
    {
        if (ids == null)
            return 0;

        var idList = string.Join(",", ids.Select(i => i.ToString()));

        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "AspectObject"},
            {"@State", state.ToString()},
            {"@IdList", idList}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    /// <summary>
    /// Change all parent id's on aspect objects from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old aspect object parent id</param>
    /// <param name="newId">New aspect object parent id</param>
    /// <returns>The number of aspect objects with the new parent id</returns>
    public async Task<int> ChangeParentId(string oldId, string newId)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "AspectObject"},
            {"@OldId", oldId},
            {"@NewId", newId}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateParentId", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
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