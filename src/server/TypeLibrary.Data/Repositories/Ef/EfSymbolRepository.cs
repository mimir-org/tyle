using System.Collections.Generic;
using System.Linq;
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

    public async Task<string> GetSymbolDataAsync(string id)
    {
        var symbol = await GetAsync(id);
        return symbol?.Data;
    }

    public IEnumerable<SymbolLibDm> Get()
    {
        return GetAll();
    }

    public SymbolLibDm Get(string id)
    {
        return FindBy(x => x.Id == id).FirstOrDefault();
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

    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}