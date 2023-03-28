using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;

namespace TypeLibrary.Data.Repositories.External;

public class DatumRepository : IQuantityDatumRepository
{
    private readonly ICacheRepository _cacheRepository;
    private readonly ISparQlWebClient _client;

    public DatumRepository(ICacheRepository cacheRepository, ISparQlWebClient client)
    {
        _cacheRepository = cacheRepository;
        _client = client;
    }

    /// <summary>
    /// Get all quantity datum range specifying
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    public async Task<List<QuantityDatumLibDm>> GetQuantityDatumRangeSpecifying()
    {
        var data = await _cacheRepository.GetOrCreateAsync("pca_quantity_datum_range_specifying", async () => await FetchDatums(SparQlWebClient.QuantityDatumRangeSpecifying, QuantityDatumType.QuantityDatumRangeSpecifying));
        return data.ToList();
    }

    /// <summary>
    /// Get all quantity datum specified scopes
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    public async Task<List<QuantityDatumLibDm>> GetQuantityDatumSpecifiedScope()
    {
        var data = await _cacheRepository.GetOrCreateAsync("pca_quantity_datum_specified_scope", async () => await FetchDatums(SparQlWebClient.QuantityDatumSpecifiedScope, QuantityDatumType.QuantityDatumSpecifiedScope));
        return data.ToList();
    }

    /// <summary>
    /// Get all quantity datum with specified provenances
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    public async Task<List<QuantityDatumLibDm>> GetQuantityDatumSpecifiedProvenance()
    {
        var data = await _cacheRepository.GetOrCreateAsync("pca_quantity_datum_specified_provenance", async () => await FetchDatums(SparQlWebClient.QuantityDatumSpecifiedProvenance, QuantityDatumType.QuantityDatumSpecifiedProvenance));
        return data.ToList();
    }

    /// <summary>
    /// Get all quantity datum regularity specified
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    public async Task<List<QuantityDatumLibDm>> GetQuantityDatumRegularitySpecified()
    {
        var data = await _cacheRepository.GetOrCreateAsync("pca_quantity_datum_regularity_specified", async () => await FetchDatums(SparQlWebClient.QuantityDatumRegularitySpecified, QuantityDatumType.QuantityDatumRegularitySpecified));
        return data.ToList();
    }

    #region Private methods

    private Task<IEnumerable<QuantityDatumLibDm>> FetchDatums(string query, QuantityDatumType type)
    {
        var data = _client.Get<PcaDatum>(SparQlWebClient.PcaEndPointProduction, query).ToList();
        var datums = data.Select(datum => new QuantityDatumLibDm { Iri = datum.Datum, Description = null, Name = datum.Datum_Label, QuantityDatumType = type, Source = "PCA" });

        return Task.FromResult(datums);
    }

    #endregion Private methods
}