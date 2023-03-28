using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IAttributeService
{
    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>List of attributes</returns>
    IEnumerable<AttributeLibCm> Get();

    /// <summary>
    /// Get an attribute by id
    /// </summary>
    /// <returns>The attribute with the given id</returns>
    AttributeLibCm Get(int id);

    /// <summary>
    /// Create a new attribute
    /// </summary>
    /// <param name="attributeAm">The attribute that should be created</param>
    /// <returns>The created attribute</returns>
    Task<AttributeLibCm> Create(AttributeLibAm attributeAm);

    /// <summary>
    /// Change attribute state
    /// </summary>
    /// <param name="id">The attribute id that should change state</param>
    /// <param name="state">The new attribute state</param>
    /// <returns>Attribute with updated state</returns>
    Task<ApprovalDataCm> ChangeState(int id, State state);

    /// <summary>
    /// Get the company id of an attribute
    /// </summary>
    /// <param name="id">The attribute id</param>
    /// <returns>Company id for the attribute</returns>
    Task<int> GetCompanyId(int id);

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
    Task<IEnumerable<QuantityDatumLibCm>> GetQuantityDatumRangeSpecifying();

    /// <summary>
    /// Get all quantity datum specified scopes
    /// </summary>
    /// <returns>List of quantity datums</returns>
    Task<IEnumerable<QuantityDatumLibCm>> GetQuantityDatumSpecifiedScope();

    /// <summary>
    /// Get all quantity datum with specified provenances
    /// </summary>
    /// <returns>List of quantity datums</returns>
    Task<IEnumerable<QuantityDatumLibCm>> GetQuantityDatumSpecifiedProvenance();

    /// <summary>
    /// Get all quantity datum regularity specified
    /// </summary>
    /// <returns>List of quantity datums</returns>
    Task<IEnumerable<QuantityDatumLibCm>> GetQuantityDatumRegularitySpecified();
}