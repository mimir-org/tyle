using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        IEnumerable<AttributeLibCm> GetAll(Aspect aspect, bool includeDeleted = false);
        Task<AttributeLibCm> Get(string id);
        Task Create(List<AttributeLibAm> attributes, bool createdBySystem = false);
        Task<AttributeLibCm> Create(AttributeLibAm attribute, bool resetVersion = false);

        Task<IEnumerable<AttributeLibCm>> GetLatestVersions(Aspect aspect);
        Task<AttributeLibCm> Update(AttributeLibAm dataAm, string id);
        Task<AttributeLibCm> UpdateState(string id, State state);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string nodeId, int companyId);

        IEnumerable<AttributePredefinedLibCm> GetPredefined();
        Task CreatePredefined(List<AttributePredefinedLibAm> predefined, bool createdBySystem = false);
        Task<IEnumerable<TypeReferenceCm>> GetAttributeReferences();

        /// <summary>
        /// Get all quantity datum range specifying
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRangeSpecifying();

        /// <summary>
        /// Get all quantity datum specified scopes
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedScope();

        /// <summary>
        /// Get all quantity datum with specified provenances
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedProvenance();

        /// <summary>
        /// Get all quantity datum regularity specified
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRegularitySpecified();
    }
}