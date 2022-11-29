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
        private readonly ISparQlWebClient _client;

        public PurposeReferenceRepository(ICacheRepository cacheRepository, ISparQlWebClient client)
        {
            _cacheRepository = cacheRepository;
            _client = client;
        }

        #region Public

        /// <summary>
        /// Get all purposes
        /// </summary>
        /// <returns>List of purpose sorted by name></returns>
        public async Task<List<PurposeLibDm>> Get()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_purposes", async () => await FetchPurposesFromPca());
            return data;
        }

        #endregion Public

        #region Private

        private Task<List<PurposeLibDm>> FetchPurposesFromPca()
        {
            var purposes = new List<PurposeLibDm>();
            var pcaPurposes = _client.Get<PcaPurpose>(SparQlWebClient.PcaEndPointStaging, SparQlWebClient.PcaPurposeAllQuery)?.OrderBy(x => x.Label, StringComparer.CurrentCultureIgnoreCase).ToList();

            if (pcaPurposes == null || !pcaPurposes.Any())
                return Task.FromResult(purposes);

            purposes.AddRange(pcaPurposes.Select(pcaPurpose => new PurposeLibDm
            {
                Name = pcaPurpose.Label,
                Iri = pcaPurpose.Imf_purpose,
                Source = "PCA"
            }));

            return Task.FromResult(purposes);
        }

        #endregion Private
    }
}