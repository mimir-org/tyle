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
    public class PurposeReferenceRepository : IPurposeReferenceRepository
    {
        private readonly ICacheRepository _cacheRepository;

        public PurposeReferenceRepository(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        #region Public

        public async Task<List<PurposeLibDm>> Get()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_purposes", async () => await FetchPurposesFromPca());
            return data;
        }

        #endregion Public

        #region Private

        private Task<List<PurposeLibDm>> FetchPurposesFromPca()
        {

            var client = new SparQlWebClient
            {
                EndPoint = SparQlWebClient.PcaEndPoint,
                Query = SparQlWebClient.PcaPurposeAllQuery
            };

            var purposes = new List<PurposeLibDm>();
            var pcaPurposes = client.Get<PcaPurpose>().ToList();

            if (!pcaPurposes.Any())
                return Task.FromResult(purposes);

            pcaPurposes = pcaPurposes.OrderBy(x => x.Quantity_Label, StringComparer.CurrentCultureIgnoreCase).ToList();
            var groups = pcaPurposes.GroupBy(x => x.Quantity).Select(group => group.ToList()).ToList();

            foreach (var group in groups)
            {
                if (!group.Any())
                    continue;

                var firstElement = group.ElementAt(0);

                var purposeDm = new PurposeLibDm
                {
                    Name = firstElement?.Quantity_Label,
                    Iri = firstElement?.Quantity,
                    Source = "PCA",
                };

                purposes.Add(purposeDm);
            }

            return Task.FromResult(purposes);
        }

        #endregion Private
    }
}