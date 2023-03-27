using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;
using TypeLibrary.Data.Repositories.Ef;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data.Repositories.External;

public class AttributePcaRepository : IAttributeReferenceRepository
{
    private readonly ISparQlWebClient _client;
    private readonly ILogger<AttributePcaRepository> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AttributePcaRepository(ISparQlWebClient client, ILogger<AttributePcaRepository> logger, IServiceProvider serviceProvider)
    {
        _client = client;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task<List<AttributeLibAm>> FetchAttributesFromReference()
    {
        var attributes = new List<AttributeLibAm>();
        var pcaAttributes = _client.Get<PcaAttribute>(SparQlWebClient.PcaEndPointProduction, SparQlWebClient.PcaAttributeAllQuery).ToList();
        
        if (!pcaAttributes.Any())
            return Task.FromResult(attributes);

        pcaAttributes = pcaAttributes.OrderBy(x => x.Quantity_Label, StringComparer.CurrentCultureIgnoreCase).ToList();
        var groups = pcaAttributes.GroupBy(x => x.Quantity).Select(group => group.ToList()).ToList();

        using var scope = _serviceProvider.CreateScope();
        var unitRepository = scope.ServiceProvider.GetService<IUnitRepository>();

        foreach (var group in groups)
        {
            if (!group.Any())
                continue;

            var firstElement = group.ElementAt(0);

            var attribute = new AttributeLibAm
            {
                Name = firstElement?.Quantity_Label,
                TypeReference = firstElement?.Quantity,
                Description = $"Attribute received from PCA at {DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InvariantCulture)} (UTC).",
                AttributeUnits = new List<AttributeUnitLibAm>()
            };

            foreach (var pcaUnit in group)
            {
                var dbUnit = unitRepository.GetByTypeReference(pcaUnit.Uom);

                if (dbUnit == null)
                {
                    _logger.LogError($"PCA Unit {pcaUnit.Uom_Label} ({pcaUnit.Uom}) not found in database.");
                    continue;
                }

                attribute.AttributeUnits.Add(new AttributeUnitLibAm
                {
                    UnitId = dbUnit.Id,
                    IsDefault = !string.IsNullOrWhiteSpace(firstElement?.Default_Uom) && (pcaUnit.Uom == firstElement.Default_Uom)
                });
            }

            attributes.Add(attribute);
        }

        return Task.FromResult(attributes);
    }
}