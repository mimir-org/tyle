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

public class EfUnitRepository : GenericRepository<TypeLibraryDbContext, UnitLibDm>, IEfUnitRepository
{
    private readonly IApplicationSettingsRepository _settings;
    private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

    public EfUnitRepository(IApplicationSettingsRepository settings, TypeLibraryDbContext dbContext, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
    {
        _settings = settings;
        _typeLibraryProcRepository = typeLibraryProcRepository;
    }

    /// <inheritdoc />
    public async Task<int> HasCompany(int id)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Unit"},
            {"@Id", id}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
        return result?.FirstOrDefault()?.CompanyId ?? 0;
    }

    /// <inheritdoc />
    public async Task<int> ChangeState(State state, ICollection<int> ids)
    {
        if (ids == null)
            return 0;

        var idList = string.Join(",", ids.Select(i => i.ToString()));

        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Unit"},
            {"@State", state.ToString()},
            {"@IdList", idList}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    /// <inheritdoc />
    public IEnumerable<UnitLibDm> Get()
    {
        return GetAll();
    }

    /// <inheritdoc />
    public UnitLibDm Get(int id)
    {
        return FindBy(x => x.Id == id).FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<UnitLibDm> Create(UnitLibDm unit)
    {
        await CreateAsync(unit);
        await SaveAsync();

        unit.Iri = $"{_settings.ApplicationSemanticUrl}/unit/{unit.Id}";
        await SaveAsync();

        Detach(unit);

        return unit;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}