using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models.External;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories.External;

public class UnitPcaRepository : IUnitReferenceRepository
{
    private readonly ISparQlWebClient _client;

    public UnitPcaRepository(ISparQlWebClient client)
    {
        _client = client;
    }

    public async Task<List<UnitLibAm>> FetchUnitsFromReference()
    {
        var units = new List<UnitLibAm>();
        var data = await _client.Get<PcaUnit>(SparQlWebClient.PcaEndPointProduction, SparQlWebClient.PcaUnitAllQuery);

        foreach (var pcaUnit in data)
        {
            var unit = new UnitLibAm
            {
                Name = pcaUnit.Uom_Label,
                TypeReference = pcaUnit.Uom,
                Symbol = pcaUnit.Default_Uom_Symbol,
                Description = $"Unit received from PCA at {DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InvariantCulture)} (UTC)."
            };

            units.Add(unit);
        }

        return units;
    }
}