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
        Task<List<AttributeQualifierLibDm>> GetQualifiers();
    }
}