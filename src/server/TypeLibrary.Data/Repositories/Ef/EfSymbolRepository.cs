using System.Collections.Generic;
using System.Runtime;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfSymbolRepository : GenericRepository<TypeLibraryDbContext, SymbolLibDm>, IEfSymbolRepository
{
    private readonly IApplicationSettingsRepository _settings;

    public EfSymbolRepository(IApplicationSettingsRepository settings, TypeLibraryDbContext dbContext) : base(dbContext)
    {
        _settings = settings;
    }

    public async Task<string> GetSymbolDataAsync(int id)
    {
        var symbol = await GetAsync(id);
        return symbol?.Data;
    }

    public IEnumerable<SymbolLibDm> Get()
    {
        return GetAll();
    }

    public async Task Create(List<SymbolLibDm> symbols, State state)
    {
        foreach (var item in symbols)
            item.State = state;

        Attach(symbols, EntityState.Added);
        await SaveAsync();
        foreach (var item in symbols)
            item.Iri = $"{_settings.ApplicationSemanticUrl}/symbol/{item.Id}";
        await SaveAsync();
        Detach(symbols);
    }

    public async Task<SymbolLibDm> Create(SymbolLibDm symbol, State state)
    {
        symbol.State = state;
        Attach(symbol, EntityState.Added);
        await SaveAsync();
        symbol.Iri = $"{_settings.ApplicationSemanticUrl}/symbol/{symbol.Id}";
        await SaveAsync();
        Detach(symbol);
        return symbol;
    }

    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }

    public void SetUnchanged(ICollection<SymbolLibDm> items)
    {
        Attach(items, EntityState.Unchanged);
    }

    public void SetDetached(ICollection<SymbolLibDm> items)
    {
        Detach(items);
    }
}