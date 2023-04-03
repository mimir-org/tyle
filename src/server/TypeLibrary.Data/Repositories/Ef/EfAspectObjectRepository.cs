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

public class EfAspectObjectRepository : GenericRepository<TypeLibraryDbContext, AspectObjectLibDm>, IEfNodeRepository
{
    private readonly IApplicationSettingsRepository _settings;
    private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

    public EfAspectObjectRepository(IApplicationSettingsRepository settings, TypeLibraryDbContext dbContext, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
    {
        _settings = settings;
        _typeLibraryProcRepository = typeLibraryProcRepository;
    }

    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The node id</param>
    /// <returns>The company id of given terminal</returns>
    public async Task<int> HasCompany(int id)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Node"},
            {"@Id", id}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
        return result?.FirstOrDefault()?.CompanyId ?? 0;
    }

    /// <summary>
    /// Change the state of the node on all listed id's
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of node id's</param>
    /// <returns>The number of nodes in given state</returns>
    public async Task<int> ChangeState(State state, ICollection<int> ids)
    {
        if (ids == null)
            return 0;

        var idList = string.Join(",", ids.Select(i => i.ToString()));

        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Node"},
            {"@State", state.ToString()},
            {"@IdList", idList}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    /// <summary>
    /// Change all parent id's on nodes from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old node parent id</param>
    /// <param name="newId">New node parent id</param>
    /// <returns>The number of nodes with the new parent id</returns>
    public async Task<int> ChangeParentId(int oldId, int newId)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Node"},
            {"@OldId", oldId},
            {"@NewId", newId}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateParentId", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    /// <summary>
    /// Check if node exists
    /// </summary>
    /// <param name="id">The id of the node</param>
    /// <returns>True if node exist</returns>
    public async Task<bool> Exist(int id)
    {
        return await Exist(x => x.Id == id);
    }

    /// <summary>
    /// Get all nodes
    /// </summary>
    /// <returns>A collection of nodes</returns>
    public IEnumerable<AspectObjectLibDm> Get()
    {
        return GetAll()
            .Include(x => x.NodeTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.NodeAttributes)
            .ThenInclude(x => x.Attribute)
            .AsSplitQuery();
    }

    /// <summary>
    /// Get node by id
    /// </summary>
    /// <param name="id">The node id</param>
    /// <returns>Node if found</returns>
    public AspectObjectLibDm Get(int id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.NodeTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.NodeAttributes)
            .ThenInclude(x => x.Attribute)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /// <summary>
    /// Create a node
    /// </summary>
    /// <param name="aspectObject">The node to be created</param>
    /// <returns>The created node</returns>
    public async Task<AspectObjectLibDm> Create(AspectObjectLibDm aspectObject)
    {
        await CreateAsync(aspectObject);
        await SaveAsync();

        if (aspectObject.FirstVersionId == 0) aspectObject.FirstVersionId = aspectObject.Id;
        aspectObject.Iri = $"{_settings.ApplicationSemanticUrl}/aspectnode/{aspectObject.Id}";
        foreach (var nodeTerminal in aspectObject.NodeTerminals)
            nodeTerminal.NodeId = aspectObject.Id;
        foreach (var nodeAttribute in aspectObject.NodeAttributes)
            nodeAttribute.NodeId = aspectObject.Id;
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