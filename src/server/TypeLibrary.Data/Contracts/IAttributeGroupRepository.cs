using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeGroupRepository
    {
        /// <summary>
        /// Get all attribute groups
        /// </summary>
        /// <returns>List of attribute groups</returns>
        Task<IEnumerable<AttributeGroupCm>> GetAttributeGroupList(string searchText = null);

        /// <summary>
        /// Get an attribute group by id
        /// </summary>
        /// <returns>The attribute group with the given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute group with the given id.</exception>
        Task<AttributeGroupCm> GetSingleAttributeGroup(string id);


        /// <summary>
        /// Create a new attribute group
        /// </summary>
        /// <param name="attributeAm">The attribute group that should be created</param>
        /// <param name="createdBy">Used to set created by value for instances where objects are not created by the user</param>
        /// <returns>The created attribute group</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if attribute group is not valid</exception>
        Task<AttributeGroupCm> Create(AttributeGroupAm attributeAm, string createdBy = null);

        /// <summary>
        /// Update an existing attribute group
        /// </summary>
        /// <param name="id">The id of the attribute group that should be updated</param>
        /// <param name="attributeAm">The new attribute group values</param>
        /// <returns>The updated attribute group</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute group with the given id.</exception>
        /// <exception cref="MimirorgBadRequestException">Throws if the new attribute group values are not valid.</exception>
        /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute group is not a draft or approved.</exception>
        Task<AttributeGroupCm> Update(string id, AttributeGroupAm attributeAm);

        /// <summary>
        ///  Delete an attribute group, it can't be approved
        /// </summary>
        /// <param name="id">The id of the attribute group to delete</param>
        /// <exception cref="MimirorgNotFoundException">Throws if the attribute group with the given id is not found.</exception>
        /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute group in question can't be deleted.</exception>
        Task Delete(string id);


        /// <summary>
        /// Change attribute group state
        /// </summary>
        /// <param name="id">The id of the attribute group that should change state</param>
        /// <param name="state">The new attribute group state</param>
        /// <param name="sendStateEmail"></param>
        /// <returns>An approval data object</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the attribute group does not exist</exception>
        /// <exception cref="MimirorgInvalidOperationException">Throws if the attribute group is already
        /// approved or contains references to deleted or unapproved units.</exception>
        Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail);


    }
}
