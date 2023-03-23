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

public class EfTerminalRepository : GenericRepository<TypeLibraryDbContext, TerminalLibDm>, IEfTerminalRepository
{
    private readonly IApplicationSettingsRepository _settings;
    private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

    public EfTerminalRepository(IApplicationSettingsRepository settings, TypeLibraryDbContext dbContext, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
    {
        _settings = settings;
        _typeLibraryProcRepository = typeLibraryProcRepository;
    }

    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The terminal id</param>
    /// <returns>The company id of given terminal</returns>
    public async Task<int> HasCompany(int id)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Terminal"},
            {"@Id", id}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
        return result?.FirstOrDefault()?.CompanyId ?? 0;
    }

    /// <summary>
    /// Change the state of the terminal on all listed id's
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of terminal id's</param>
    /// <returns>The number of terminals in given state</returns>
    public async Task<int> ChangeState(State state, ICollection<int> ids)
    {
        if (ids == null)
            return 0;

        var idList = string.Join(",", ids.Select(i => i.ToString()));

        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Terminal"},
            {"@State", state.ToString()},
            {"@IdList", idList}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    /// <summary>
    /// Change all parent id's on terminals from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old terminal parent id</param>
    /// <param name="newId">New terminal parent id</param>
    /// <returns>The number of terminal with the new parent id</returns>
    public async Task<int> ChangeParentId(int oldId, int newId)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Terminal"},
            {"@OldId", oldId},
            {"@NewId", newId}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateParentId", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    /// <summary>
    /// Check if terminal exists
    /// </summary>
    /// <param name="id">The id of the terminal</param>
    /// <returns>True if terminal exist</returns>
    public async Task<bool> Exist(int id)
    {
        return await Exist(x => x.Id == id);
    }

    /// <summary>
    /// Get all terminals
    /// </summary>
    /// <returns>A collection of terminals</returns>
    public IEnumerable<TerminalLibDm> Get()
    {
        return GetAll()
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .AsSplitQuery();
    }

    /// <summary>
    /// Get terminal by id
    /// </summary>
    /// <param name="id">The terminal id</param>
    /// <returns>Terminal if found</returns>
    public TerminalLibDm Get(int id)
    {
        var terminal = FindBy(x => x.Id == id)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .AsSplitQuery()
            .FirstOrDefault();
        return terminal;
    }

    /// <summary>
    /// Create a terminal in database
    /// </summary>
    /// <param name="terminal">The terminal to be created</param>
    /// <returns>The created terminal</returns>
    public async Task<TerminalLibDm> Create(TerminalLibDm terminal)
    {
        await CreateAsync(terminal);
        await SaveAsync();

        if (terminal.FirstVersionId == 0) terminal.FirstVersionId = terminal.Id;
        terminal.Iri = $"{_settings.ApplicationSemanticUrl}/terminal/{terminal.Id}";
        await SaveAsync();

        return terminal;
    }

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}