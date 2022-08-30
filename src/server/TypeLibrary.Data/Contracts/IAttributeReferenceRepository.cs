using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeReferenceRepository
    {
        /// <summary>
        /// Get all attribute references
        /// </summary>
        /// <returns>A collection of attribute references</returns>
        Task<List<AttributeReferenceDm>> Get();
    }
}