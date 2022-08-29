using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;

namespace TypeLibrary.Data.Repositories.External
{
    public class AttributeReferenceRepository : IAttributeReferenceRepository
    {
        private readonly IApplicationSettingsRepository _settings;
        private readonly ICacheRepository _cacheRepository;

        public AttributeReferenceRepository(IApplicationSettingsRepository settings, ICacheRepository cacheRepository)
        {
            _settings = settings;
            _cacheRepository = cacheRepository;
        }

        #region Public 

        public async Task<List<AttributeReferenceDm>> Get()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_attributes", async () => await FetchAttributesFromPca());
            return data;
        }

        #endregion Public


        #region Private methods

        private Task<List<AttributeReferenceDm>> FetchAttributesFromPca()
        {
            var client = new SparQlWebClient
            {
                EndPoint = SparQlWebClient.PcaEndPoint,
                Query = SparQlWebClient.PcaAttributeAllQuery
            };

            var attributes = new List<AttributeReferenceDm>();
            var data = client.Get<PcaAttribute>().ToList();

            if (!data.Any())
                return Task.FromResult(attributes);

            data = data.OrderBy(x => x.Quantity_Label, StringComparer.CurrentCultureIgnoreCase).ToList();

            foreach (var pcaUnit in data)
            {
                var attributeReferenceDm = new AttributeReferenceDm
                {
                    Name = pcaUnit.Quantity_Label,
                    Iri = pcaUnit.Quantity,
                    Source = "PCA"
                };

                attributes.Add(attributeReferenceDm);
            }

            return Task.FromResult(attributes);
        }

        #endregion
    }
}