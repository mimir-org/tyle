
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfTerminalRepository : GenericRepository<TypeLibraryDbContext, TerminalType>, IEfTerminalRepository
{
    public EfTerminalRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /*/// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var terminal = await GetAsync(id);
        terminal.State = state;
        await SaveAsync();
        Detach(terminal);
    }*/

    /// <inheritdoc />
    public IEnumerable<TerminalType> Get()
    {
        return GetAll()
            .Include(x => x.Classifiers)
            .ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Medium)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Predicate)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Units)
            .ThenInclude(x => x.Unit)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.ValueConstraint)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public TerminalType Get(Guid id)
    {
        var terminal = FindBy(x => x.Id == id)
            .Include(x => x.Classifiers)
            .ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Medium)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Predicate)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Units)
            .ThenInclude(x => x.Unit)
            .Include(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.ValueConstraint)
            .AsSplitQuery()
            .FirstOrDefault();
        return terminal;
    }

    /// <inheritdoc />
    public async Task<TerminalType> Create(TerminalType terminal)
    {
        await CreateAsync(terminal);
        await SaveAsync();

        Detach(terminal);

        return terminal;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}