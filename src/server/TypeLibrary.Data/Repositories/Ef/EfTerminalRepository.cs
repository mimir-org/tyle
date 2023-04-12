using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfTerminalRepository : GenericRepository<TypeLibraryDbContext, TerminalLibDm>, IEfTerminalRepository
{
    public EfTerminalRepository(TypeLibraryDbContext dbContext) : base(dbContext)
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
        var terminal = await GetAsync(id);
        terminal.State = state;
        await SaveAsync();
        Detach(terminal);
    }

    /// <inheritdoc />
    public async Task<int> ChangeState(State state, ICollection<string> ids)
    {
        var terminalsToChange = new List<TerminalLibDm>();
        foreach (var id in ids)
        {
            var terminal = await GetAsync(id);
            terminal.State = state;
            terminalsToChange.Add(terminal);
        }

        await SaveAsync();
        Detach(terminalsToChange);

        return terminalsToChange.Count;
    }

    /// <summary>
    /// Change all parent id's on terminals from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old terminal parent id</param>
    /// <param name="newId">New terminal parent id</param>
    /// <returns>The number of terminal with the new parent id</returns>
    public async Task<int> ChangeParentId(string oldId, string newId)
    {
        var affectedTerminals = FindBy(x => x.ParentId == oldId);

        foreach (var terminal in affectedTerminals)
        {
            terminal.ParentId = newId;
        }

        await SaveAsync();
        Detach(affectedTerminals.ToList());

        return affectedTerminals.Count();
    }

    /// <summary>
    /// Check if terminal exists
    /// </summary>
    /// <param name="id">The id of the terminal</param>
    /// <returns>True if terminal exist</returns>
    public async Task<bool> Exist(string id)
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
            .Include(x => x.Attributes);
    }

    /// <summary>
    /// Get terminal by id
    /// </summary>
    /// <param name="id">The terminal id</param>
    /// <returns>Terminal if found</returns>
    public TerminalLibDm Get(string id)
    {
        var terminal = FindBy(x => x.Id == id)
            .Include(x => x.Attributes)
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