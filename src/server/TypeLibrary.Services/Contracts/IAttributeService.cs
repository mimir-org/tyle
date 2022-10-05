using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        /// <summary>
        /// Get the latest version of an attribute based on given id
        /// </summary>
        /// <param name="id">The id of the attribute</param>
        /// <returns>The latest version of the attribute of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute with the given id, and that attribute is at the latest version.</exception>
        AttributeLibCm GetLatestVersion(string id);

        /// <summary>
        /// Get the latest attribute versions
        /// </summary>
        /// <returns>A collection of attributes</returns>
        IEnumerable<AttributeLibCm> GetLatestVersions(Aspect aspect);

        /// <summary>
        /// Create an new attribute
        /// </summary>
        /// <param name="attributeAm">The attribute that should be created</param>
        /// <returns>The created attribute</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if attribute is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if attribute already exist</exception>
        /// <remarks>Remember that creating a new attribute could be creating a new version of existing node.
        /// They will have the same first version id, but have different version and id.</remarks>
        Task<AttributeLibCm> Create(AttributeLibAm attributeAm);

        /// <summary>
        /// Update an attribute if the data is allowed to be changed.
        /// </summary>
        /// <param name="attributeAm">The attribute to update</param>
        /// <returns>The updated attribute</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the attribute does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        Task<AttributeLibCm> Update(AttributeLibAm attributeAm);

        /// <summary>
        /// Change attribute state
        /// </summary>
        /// <param name="id">The attribute id that should change the state</param>
        /// <param name="state">The new attribute state</param>
        /// <returns>Attribute with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the attribute does not exist on latest version</exception>
        Task<AttributeLibCm> ChangeState(string id, State state);

        /// <summary>
        /// Get node existing company id for terminal by id
        /// </summary>
        /// <param name="id">The node id</param>
        /// <returns>Company id for node</returns>
        Task<int> GetCompanyId(string id);

        /// <summary>
        /// Get predefined attributes
        /// </summary>
        /// <returns>A List of predefined attributes</returns>
        IEnumerable<AttributePredefinedLibCm> GetPredefined();

        /// <summary>
        /// Create predefined attributes
        /// </summary>
        /// <param name="predefined"></param>
        /// <returns>A List of created predefined attributes</returns>
        Task CreatePredefined(List<AttributePredefinedLibAm> predefined);

        /// <summary>
        /// Get attribute references
        /// </summary>
        /// <returns>A List of attribute references</returns>
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