using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;

namespace TypeLibrary.Data.Repositories.External;

public class QuantityDatumPcaRepository : IQuantityDatumReferenceRepository
{
    private readonly ISparQlWebClient _client;

    public QuantityDatumPcaRepository(ISparQlWebClient client)
    {
        _client = client;
    }

    public Task<List<QuantityDatumLibAm>> FetchQuantityDatumsFromReference()
    {
        var quantityDatums = new List<QuantityDatumLibAm>();
        var quantityDatumQueries = new List<string>
        {
            SparQlWebClient.QuantityDatumRangeSpecifying,
            SparQlWebClient.QuantityDatumSpecifiedScope,
            SparQlWebClient.QuantityDatumSpecifiedProvenance,
            SparQlWebClient.QuantityDatumRegularitySpecified
        };
        var quantityDatumTypes = new List<QuantityDatumType>
        {
            QuantityDatumType.QuantityDatumRangeSpecifying,
            QuantityDatumType.QuantityDatumSpecifiedScope,
            QuantityDatumType.QuantityDatumSpecifiedProvenance,
            QuantityDatumType.QuantityDatumRegularitySpecified
        };

        for (var i = 0; i < quantityDatumQueries.Count; i++)
        {
            var data = _client.Get<PcaDatum>(SparQlWebClient.PcaEndPointProduction, quantityDatumQueries[i]).ToList();
            quantityDatums.AddRange(data.Select(datum => new QuantityDatumLibAm
            {
                Name = datum.Datum_Label,
                TypeReference = datum.Datum,
                Description = $"Quantity datum received from PCA at {DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InvariantCulture)} (UTC).",
                QuantityDatumType = quantityDatumTypes[i]
            }));
        }

        return Task.FromResult(quantityDatums);
    }
}