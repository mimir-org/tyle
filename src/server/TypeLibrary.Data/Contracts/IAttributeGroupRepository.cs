using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeGroupRepository
    {
        /// <summary>
        /// Get all attribute groups
        /// </summary>
        /// <returns>List of attribute groups</returns>
        IEnumerable<AttributeGroupLibDm> GetAttributeGroupList();

        /// <summary>
        /// Get an attribute group by id
        /// </summary>
        /// <returns>The attribute group with the given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no attribute group with the given id.</exception>
        AttributeGroupLibDm GetSingleAttributeGroup(string id);


        /// <summary>
        /// Create a new attribute group
        /// </summary>
        /// <param name="attributeAm">The attribute group that should be created</param>    
        /// <returns>The created attribute group</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if attribute group is not valid</exception>
        Task<AttributeGroupLibDm> Create(AttributeGroupLibDm attributeAm);

        /// <summary>
        /// Clear all entity framework change trackers
        /// </summary>
        void ClearAllChangeTrackers();


    }
}