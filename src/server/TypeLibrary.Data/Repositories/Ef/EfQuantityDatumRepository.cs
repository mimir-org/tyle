using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfQuantityDatumRepository : GenericRepository<TypeLibraryDbContext, QuantityDatumLibDm>, IEfQuantityDatumRepository
{
    private readonly IApplicationSettingsRepository _settings;
    private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

    public EfQuantityDatumRepository(IApplicationSettingsRepository settings, TypeLibraryDbContext dbContext, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
    {
        _settings = settings;
        _typeLibraryProcRepository = typeLibraryProcRepository;
    }

    /// <inheritdoc />
    public async Task<int> HasCompany(int id)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "QuantityDatum"},
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
            {"@TableName", "QuantityDatum"},
            {"@State", state.ToString()},
            {"@IdList", idList}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> Get()
    {
        return GetAll();
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumRangeSpecifying()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumRangeSpecifying);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumSpecifiedScope()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumSpecifiedScope);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumSpecifiedProvenance()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumSpecifiedProvenance);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumRegularitySpecified()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumRegularitySpecified);
    }

    /// <inheritdoc />
    public async Task<QuantityDatumLibDm> Create(QuantityDatumLibDm quantityDatum)
    {
        await CreateAsync(quantityDatum);
        await SaveAsync();

        quantityDatum.Iri = $"{_settings.ApplicationSemanticUrl}/quantitydatum/{quantityDatum.Id}";
        await SaveAsync();

        Detach(quantityDatum);

        return quantityDatum;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}