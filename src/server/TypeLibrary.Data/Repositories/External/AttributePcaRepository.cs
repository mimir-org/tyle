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
using TypeLibrary.Data.Models.External;

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

    public async Task<List<AttributeTypeRequest>> FetchAttributesFromReference()
    {
        var attributes = new List<AttributeTypeRequest>();
        var pcaAttributes = await _client.Get<PcaAttribute>(SparQlWebClient.PcaEndPointProduction, SparQlWebClient.PcaAttributeAllQuery);

        if (!pcaAttributes.Any())
            return attributes;

        pcaAttributes = pcaAttributes.OrderBy(x => x.Quantity_Label, StringComparer.CurrentCultureIgnoreCase).ToList();
        var groups = pcaAttributes.GroupBy(x => x.Quantity).Select(group => group.ToList()).ToList();

        using var scope = _serviceProvider.CreateScope();

        foreach (var group in groups)
        {
            if (!group.Any())
                continue;

            var firstElement = group.ElementAt(0);

            var attribute = new AttributeTypeRequest
            {
                Name = firstElement?.Quantity_Label,
                //TypeReference = firstElement?.Quantity,
                Description = $"Attribute received from PCA at {DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InvariantCulture)} (UTC)."
                //AttributeUnits = new List<AttributeUnitLibAm>()
            };

            attributes.Add(attribute);
        }

        return attributes;
    }
}