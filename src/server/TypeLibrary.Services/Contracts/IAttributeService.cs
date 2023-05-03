using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
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
    /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute with the given id.</exception>
    AttributeLibCm Get(string id);

    /// <summary>
    /// Create a new attribute
    /// </summary>
    /// <param name="attributeAm">The attribute that should be created</param>
    /// <param name="createdBy">Used to set created by value for instances where objects are not created by the user</param>
    /// <returns>The created attribute</returns>
    Task<AttributeLibCm> Create(AttributeLibAm attributeAm, string createdBy = null);

    /// <summary>
    /// Update an existing attribute
    /// </summary>
    /// <param name="id">The id of the attribute that should be updated</param>
    /// <param name="attributeAm">The new attribute values</param>
    /// <returns>The updated attribute</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute with the given id.</exception>
    /// <exception cref="MimirorgBadRequestException">Throws if the new attribute values are not valid.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute is not a draft or approved.</exception>
    Task<AttributeLibCm> Update(string id, AttributeLibAm attributeAm);

    /// <summary>
    /// Change attribute state
    /// </summary>
    /// <param name="id">The id of the attribute that should change state</param>
    /// <param name="state">The new attribute state</param>
    /// <returns>An approval data object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the attribute does not exist</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute is already
    /// approved or contains references to deleted or unapproved units.</exception>
    Task<ApprovalDataCm> ChangeState(string id, State state);

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