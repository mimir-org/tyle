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
    AttributeLibCm Get(string id);

    /// <summary>
    /// Create a new attribute
    /// </summary>
    /// <param name="attributeAm">The attribute that should be created</param>
    /// <param name="createdBySync">Set to true if the attribute is created by a sync job</param>
    /// <returns>The created attribute</returns>
    Task<AttributeLibCm> Create(AttributeLibAm attributeAm, bool createdBySync = false);

    /// <summary>
    /// Update an existing attribute
    /// </summary>
    /// <param name="id">The id of the attribute that should be updated</param>
    /// <param name="attributeAm">The new attribute values</param>
    /// <returns>The updated attribute</returns>
    Task<AttributeLibCm> Update(string id, AttributeLibAm attributeAm);

    /// <summary>
    /// Change attribute state
    /// </summary>
    /// <param name="id">The attribute id that should change state</param>
    /// <param name="state">The new attribute state</param>
    /// <returns>Attribute with updated state</returns>
    Task<ApprovalDataCm> ChangeState(string id, State state);

    /// <summary>
    /// Get the company id of an attribute
    /// </summary>
    /// <param name="id">The attribute id</param>
    /// <returns>Company id for the attribute</returns>
    int GetCompanyId(string id);

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
}