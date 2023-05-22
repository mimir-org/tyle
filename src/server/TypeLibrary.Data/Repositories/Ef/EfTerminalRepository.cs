
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

    /// <inheritdoc />
    public async Task<bool> Exist(string id)
    {
        return await Exist(x => x.Id == id);
    }

    /// <inheritdoc />
    public IEnumerable<TerminalLibDm> Get()
    {
        return GetAll()
            .Include(x => x.Attributes);
    }

    /// <inheritdoc />
    public TerminalLibDm Get(string id)
    {
        var terminal = FindBy(x => x.Id == id)
            .Include(x => x.Attributes)
            .FirstOrDefault();
        return terminal;
    }

    /// <inheritdoc />
    public async Task<TerminalLibDm> Create(TerminalLibDm terminal)
    {
        await CreateAsync(terminal);
        await SaveAsync();

        return terminal;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}