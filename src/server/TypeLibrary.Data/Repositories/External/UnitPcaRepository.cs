using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
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
    
    public Task<List<UnitLibDm>> FetchUnitsFromReference()
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
                TypeReference = pcaUnit.Uom,
                Symbol = pcaUnit.Default_Uom_Symbol,
                State = State.ApprovedGlobal,
                Description = $"Unit recovered from PCA at {DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InvariantCulture)} (UTC).",
                Created = DateTime.UtcNow,
                CreatedBy = "PCA sync"
            };

            units.Add(unit);
        }

        return Task.FromResult(units);
    }
}