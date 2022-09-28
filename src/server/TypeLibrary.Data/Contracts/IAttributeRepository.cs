using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeRepository
    {

        #region Attribute

        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The attribute id</param>
        /// <returns>The company id of given attribute</returns>
        Task<int> HasCompany(string id);

        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns>A collection of attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        IEnumerable<AttributeLibDm> Get();

        /// <summary>
        /// Get attribute by id
        /// </summary>
        /// <param name="id">The attribute id</param>
        /// <returns>If exist it returns the attribute, otherwise it returns null</returns>
        Task<AttributeLibDm> Get(string id);

        /// <summary>
        /// Change the state of the attribute on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of attribute id's</param>
        /// <returns>The number of attributes in given state</returns>
        Task<int> ChangeState(State state, ICollection<string> ids);

        /// <summary>
        /// Change all parent id's on attributes from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old attribute parent id</param>
        /// <param name="newId">New attribute parent id</param>
        /// <returns>The number of attributes with the new parent id</returns>
        Task<int> ChangeParentId(string oldId, string newId);

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        Task<AttributeLibDm> Create(AttributeLibDm attribute);

        /// <summary>
        /// Check if an attribute already exist
        /// </summary>
        /// <param name="id">The attribute id to check if exist</param>
        /// <returns>True if attribute already exist</returns>
        Task<bool> Exist(string id);

        void SetUnchanged(ICollection<AttributeLibDm> items);
        void SetDetached(ICollection<AttributeLibDm> items);

        void ClearAllChangeTrackers();

        #endregion Attribute
    }
}