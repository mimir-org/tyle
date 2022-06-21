using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeQualifierRepository
    {
        /// <summary>
        /// Get all attribute qualifiers
        /// </summary>
        /// <returns>A collection of attribute qualifiers</returns>
        /// <remarks>Only qualifiers that is not deleted will be returned</remarks>
        IEnumerable<AttributeQualifierLibDm> GetQualifiers();

        /// <summary>
        /// Creates a new attribute qualifier
        /// </summary>
        /// <param name="qualifier">The attribute qualifier that should be created</param>
        /// <returns>An attribute qualifier</returns>
        Task<AttributeQualifierLibDm> CreateQualifier(AttributeQualifierLibDm qualifier);
    }
}