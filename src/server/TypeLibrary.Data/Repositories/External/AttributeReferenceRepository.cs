using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data.Repositories.External;

public class AttributeReferenceRepository : IAttributeReferenceRepository
{
    private readonly ICacheRepository _cacheRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly ISparQlWebClient _client;

    public AttributeReferenceRepository(ICacheRepository cacheRepository, IUnitRepository unitRepository, ISparQlWebClient client)
    {
        _cacheRepository = cacheRepository;
        _unitRepository = unitRepository;
        _client = client;
    }

    #region Public 

    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>A collection of attributes</returns>
    public async Task<List<AttributeLibDm>> Get()
    {
        var data = await _cacheRepository.GetOrCreateAsync("pca_attributes", async () => await FetchAttributesFromPca());
        return data;
    }

    #endregion Public

    #region Private

    private Task<List<AttributeLibDm>> FetchAttributesFromPca()
    {
        var attributes = new List<AttributeLibDm>();
        var pcaAttributes = _client.Get<PcaAttribute>(SparQlWebClient.PcaEndPointProduction, SparQlWebClient.PcaAttributeAllQuery).ToList();
        var pcaUnits = _unitRepository.Get().Result;

        if (!pcaAttributes.Any())
            return Task.FromResult(attributes);

        pcaAttributes = pcaAttributes.OrderBy(x => x.Quantity_Label, StringComparer.CurrentCultureIgnoreCase).ToList();
        var groups = pcaAttributes.GroupBy(x => x.Quantity).Select(group => group.ToList()).ToList();

        foreach (var group in groups)
        {
            if (!group.Any())
                continue;

            var firstElement = group.ElementAt(0);

            var attributeDm = new AttributeLibDm
            {
                Name = firstElement?.Quantity_Label,
                Iri = firstElement?.Quantity,
                Source = "PCA",
                Units = new List<UnitLibDm>()
            };

            foreach (var pcaUnit in group)
            {
                attributeDm.Units.Add(new UnitLibDm
                {
                    Name = pcaUnit.Uom_Label,
                    Iri = pcaUnit.Uom,
                    Symbol = pcaUnits.FirstOrDefault(x => x.Iri == pcaUnit.Uom)?.Symbol,
                    Source = "PCA",
                    IsDefault = !string.IsNullOrWhiteSpace(firstElement?.Default_Uom) && (pcaUnit.Uom == firstElement.Default_Uom)
                });
            }

            attributes.Add(attributeDm);
        }

        return Task.FromResult(attributes);
    }

    #endregion Private
}