using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        /// <summary>
        /// Get all attributes and their units
        /// </summary>
        /// <returns>List of attributes and their units></returns>
        Task<ICollection<AttributeLibCm>> Get();

        /// <summary>
        /// Get predefined attributes
        /// </summary>
        /// <returns>List of predefined attributes</returns>
        IEnumerable<AttributePredefinedLibCm> GetPredefined();

        /// <summary>
        /// Create predefined attributes
        /// </summary>
        /// <param name="predefined"></param>
        /// <returns>Created predefined attribute</returns>
        Task CreatePredefined(List<AttributePredefinedLibAm> predefined);

        /// <summary>
        /// Get all quantity datum range specifying
        /// </summary>
        /// <returns>List of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRangeSpecifying();

        /// <summary>
        /// Get all quantity datum specified scopes
        /// </summary>
        /// <returns>List of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedScope();

        /// <summary>
        /// Get all quantity datum with specified provenances
        /// </summary>
        /// <returns>List of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedProvenance();

        /// <summary>
        /// Get all quantity datum regularity specified
        /// </summary>
        /// <returns>List of quantity datums</returns>
        Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRegularitySpecified();
    }
}