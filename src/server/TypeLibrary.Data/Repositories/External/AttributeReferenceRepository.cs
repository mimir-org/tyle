using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data.Repositories.External
{
    public class AttributeReferenceRepository : IAttributeReferenceRepository
    {
        private readonly ICacheRepository _cacheRepository;

        public AttributeReferenceRepository(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        #region Public 

        public async Task<List<TypeReferenceDm>> Get()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_attributes", async () => await FetchAttributesFromPca());
            return data;
        }

        #endregion Public


        #region Private methods

        private Task<List<TypeReferenceDm>> FetchAttributesFromPca()
        {
            var client = new SparQlWebClient
            {
                EndPoint = SparQlWebClient.PcaEndPoint,
                Query = SparQlWebClient.PcaAttributeAllQuery
            };

            var attributes = new List<TypeReferenceDm>();
            var data = client.Get<PcaAttribute>().ToList();

            if (!data.Any())
                return Task.FromResult(attributes);

            data = data.OrderBy(x => x.Quantity_Label, StringComparer.CurrentCultureIgnoreCase).ToList();
            var groups = data.GroupBy(x => x.Quantity).Select(group => group.ToList()).ToList();

            foreach (var group in groups)
            {
                if (!group.Any())
                    continue;

                var firstElement = group.ElementAt(0);

                var attributeReferenceDm = new TypeReferenceDm
                {
                    Name = firstElement?.Quantity_Label,
                    Iri = firstElement?.Quantity,
                    Source = "PCA",
                    Units = new List<TypeReferenceSub>()
                };

                foreach (var pcaUnit in group)
                {
                    attributeReferenceDm.Units.Add(new TypeReferenceSub
                    {
                        Name = pcaUnit.Uom_Label,
                        Iri = pcaUnit.Uom,
                        IsDefault = !string.IsNullOrWhiteSpace(firstElement?.Default_Uom) && (pcaUnit.Uom == firstElement.Default_Uom)
                    });
                }

                attributes.Add(attributeReferenceDm);
            }

            return Task.FromResult(attributes);
        }

        #endregion
    }
}