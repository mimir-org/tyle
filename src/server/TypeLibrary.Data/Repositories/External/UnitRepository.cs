using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;

namespace TypeLibrary.Data.Repositories.External;

public class UnitRepository : IUnitRepository
{
    private readonly ICacheRepository _cacheRepository;
    private readonly ISparQlWebClient _client;

    public UnitRepository(ICacheRepository cacheRepository, ISparQlWebClient client)
    {
        _cacheRepository = cacheRepository;
        _client = client;
    }

    #region Public methods

    /// <summary>
    /// Get all units
    /// </summary>
    /// <returns>A collection of units</returns>
    /// <remarks>Only units that is not deleted will be returned</remarks>
    public async Task<List<UnitLibDm>> Get()
    {
        var data = await _cacheRepository.GetOrCreateAsync("pca_units", async () => await FetchUnitsFromPca());
        return data.OrderBy(x => x.Name).ToList();
    }

    /// <summary>
    /// Create an unit
    /// </summary>
    /// <param name="unit">The unit to be created</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task<UnitLibDm> CreateUnit(UnitLibDm unit)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Private methods

    private Task<List<UnitLibDm>> FetchUnitsFromPca()
    {
        var units = new List<UnitLibDm>();
        var data = _client.Get<PcaUnit>(SparQlWebClient.PcaEndPointProduction, SparQlWebClient.PcaUnitAllQuery).ToList();

        if (!data.Any())
            return Task.FromResult(units);

        foreach (var pcaUnit in data)
        {
            var unit = new UnitLibDm
            {
                Name = pcaUnit.Uom_Label,
                Iri = pcaUnit.Uom,
                Symbol = pcaUnit.Default_Uom_Symbol,
                Source = "PCA"
            };

            units.Add(unit);
        }

        return Task.FromResult(units);
    }

    #endregion
}