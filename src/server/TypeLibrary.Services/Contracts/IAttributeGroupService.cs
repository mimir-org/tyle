using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeGroupService
    {
        /// <summary>
        /// Get all attribute groups
        /// </summary>
        /// <returns>List of attribute groups</returns>
        IEnumerable<AttributeGroupLibCm> GetAttributeGroupList();

        /// <summary>
        /// Get an attribute group by id
        /// </summary>
        /// <returns>The attribute group with the given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute group with the given id.</exception>
        AttributeGroupLibCm GetSingleAttributeGroup(string id);


        /// <summary>
        /// Create a new attribute group
        /// </summary>
        /// <param name="attributeAm">The attribute group that should be created</param>
        /// <param name="createdBy">Used to set created by value for instances where objects are not created by the user</param>
        /// <returns>The created attribute group</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if attribute group is not valid</exception>
        Task<AttributeGroupLibCm> Create(AttributeGroupLibAm attributeAm);

        /// <summary>
        /// Update an existing attribute group
        /// </summary>
        /// <param name="id">The id of the attribute group that should be updated</param>
        /// <param name="attributeAm">The new attribute group values</param>
        /// <returns>The updated attribute group</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute group with the given id.</exception>
        /// <exception cref="MimirorgBadRequestException">Throws if the new attribute group values are not valid.</exception>
        /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute group is not a draft or approved.</exception>
        Task<AttributeGroupLibCm> Update(string id, AttributeGroupLibAm attributeAm);

        /// <summary>
        ///  Delete an attribute group
        /// </summary>
        /// <param name="id">The id of the attribute group to delete</param>
        /// <exception cref="MimirorgNotFoundException">Throws if the attribute group with the given id is not found.</exception>
        /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute group in question can't be deleted.</exception>
        Task Delete(string id);

    }
}
