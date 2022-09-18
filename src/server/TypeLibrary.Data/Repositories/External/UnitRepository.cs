using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;

namespace TypeLibrary.Data.Repositories.External
{
    public class UnitRepository : IUnitRepository
    {
        private readonly IApplicationSettingsRepository _settings;
        private readonly ICacheRepository _cacheRepository;

        public UnitRepository(IApplicationSettingsRepository settings, ICacheRepository cacheRepository)
        {
            _settings = settings;
            _cacheRepository = cacheRepository;
        }

        #region Public methods

        /// <summary>
        /// Get all units
        /// </summary>
        /// <returns>A collection of units</returns>
        /// <remarks>Only units that is not deleted will be returned</remarks>
        public async Task<List<UnitLibDm>> Get()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_units", async () => await FetchUnitsFromPca());
            return data.OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Create an unit
        /// </summary>
        /// <param name="unit">The unit to be created</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<UnitLibDm> CreateUnit(UnitLibDm unit)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private methods

        private Task<List<UnitLibDm>> FetchUnitsFromPca()
        {
            var client = new SparQlWebClient
            {
                EndPoint = SparQlWebClient.PcaEndPoint,
                Query = SparQlWebClient.PcaUnitAllQuery
            };

            var units = new List<UnitLibDm>();
            var data = client.Get<PcaUnit>().ToList();

            if (!data.Any())
                return Task.FromResult(units);

            foreach (var pcaUnit in data)
            {
                var id = $"{pcaUnit.Uom_Label}".CreateMd5();
                var iri = $"{_settings.ApplicationSemanticUrl}/unit/{id}";

                var typeReferences = new List<TypeReferenceAm>
                {
                    new()
                    {
                        Iri = pcaUnit.Uom,
                        Name = pcaUnit.Uom_Label,
                        Source = "PCA"
                    }
                };

                var unit = new UnitLibDm
                {
                    Id = id,
                    Iri = iri,
                    Name = pcaUnit.Uom_Label,
                    Description = $"{pcaUnit.Default_Uom_Symbol}",
                    TypeReferences = typeReferences.ConvertToString(),
                    Symbol = pcaUnit.Default_Uom_Symbol
                };

                units.Add(unit);
            }

            return Task.FromResult(units);
        }

        #endregion
    }
}