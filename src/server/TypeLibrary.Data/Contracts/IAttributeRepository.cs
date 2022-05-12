using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeRepository
    {
        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns>A collection of attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        IEnumerable<AttributeLibDm> GetAllAttributes();

        /// <summary>
        /// Get attribute by id
        /// </summary>
        /// <param name="id">The id of the attribute</param>
        /// <returns>An attribute</returns>
        Task<AttributeLibDm> GetAttribute(string id);

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        Task<AttributeLibDm> CreateAttribute(AttributeLibDm attribute);

        /// <summary>
        /// Get all predefined attributes
        /// </summary>
        /// <returns>A collection of predefined attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        IEnumerable<AttributePredefinedLibDm> GetAllAttributePredefine();

        /// <summary>
        /// Create a predefined attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        Task<AttributePredefinedLibDm> CreateAttributePredefined(AttributePredefinedLibDm attribute);
    }
}
