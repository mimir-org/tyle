using System;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    AttributeLibCm Get(Guid id);

    /// <summary>
    /// Create a new attribute
    /// </summary>
    /// <param name="attributeAm">The attribute that should be created</param>
    /// <returns>The created attribute</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if attribute is not valid</exception>
    Task<AttributeLibCm> Create(AttributeTypeRequest attributeAm);

    /// <summary>
    /// Update an existing attribute
    /// </summary>
    /// <param name="id">The id of the attribute that should be updated</param>
    /// <param name="attributeAm">The new attribute values</param>
    /// <returns>The updated attribute</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute with the given id.</exception>
    /// <exception cref="MimirorgBadRequestException">Throws if the new attribute values are not valid.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute is not a draft or approved.</exception>
    Task<AttributeLibCm> Update(Guid id, AttributeTypeRequest attributeAm);

    /// <summary>
    ///  Delete an attribute, it can't be approved
    /// </summary>
    /// <param name="id">The id of the attribute to delete</param>
    /// <exception cref="MimirorgNotFoundException">Throws if the attribute with the given id is not found.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute in question can't be deleted.</exception>
    Task Delete(Guid id);

    /*/// <summary>
    /// Change attribute state
    /// </summary>
    /// <param name="id">The id of the attribute that should change state</param>
    /// <param name="state">The new attribute state</param>
    /// <param name="sendStateEmail"></param>
    /// <returns>An approval data object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the attribute does not exist</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute is already
    /// approved or contains references to deleted or unapproved units.</exception>
    Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail);

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
    Task CreatePredefined(List<AttributePredefinedLibAm> predefined);*/
}