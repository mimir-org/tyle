
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
    public IEnumerable<TerminalLibDm> Get()
    {
        return GetAll()
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public TerminalLibDm Get(string id)
    {
        var terminal = FindBy(x => x.Id == id)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .AsSplitQuery()
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