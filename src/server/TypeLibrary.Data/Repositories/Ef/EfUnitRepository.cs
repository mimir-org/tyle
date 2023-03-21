using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfUnitRepository : GenericRepository<TypeLibraryDbContext, UnitLibDm>, IEfUnitRepository
{
    private readonly IApplicationSettingsRepository _settings;

    public EfUnitRepository(IApplicationSettingsRepository settings, TypeLibraryDbContext dbContext) : base(dbContext)
    {
        _settings = settings;
    }

    public IEnumerable<UnitLibDm> Get()
    {
        return GetAll();
    }

    public async Task<UnitLibDm> Create(UnitLibDm unit)
    {
        await CreateAsync(unit);
        await SaveAsync();

        unit.Iri = $"{_settings.ApplicationSemanticUrl}/unit/{unit.Id}";
        await SaveAsync();

        Detach(unit);

        return unit;
    }

    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}