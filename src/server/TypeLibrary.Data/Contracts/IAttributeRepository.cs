using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeRepository
    {

        #region Attribute

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
        /// Update Sate on an attribute
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns>Void</returns>
        Task UpdateState(string id, State state);

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <param name="state"></param>
        /// <returns>An attribute</returns>
        Task<AttributeLibDm> Create(AttributeLibDm attribute, State state);

        /// <summary>
        /// Mark an attribute with State 'Deleted'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Remove(string id);

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