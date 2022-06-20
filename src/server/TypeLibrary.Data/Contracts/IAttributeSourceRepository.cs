using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeSourceRepository
    {
        /// <summary>
        /// Get all attribute sources
        /// </summary>
        /// <returns>A collection of attribute sources</returns>
        /// <remarks>Only sources that is not deleted will be returned</remarks>
        IEnumerable<AttributeSourceLibDm> GetSources();

        /// <summary>
        /// Creates a new attribute source
        /// </summary>
        /// <param name="source">The attribute qualifier that should be created</param>
        /// <returns>An attribute qualifier</returns>
        Task<AttributeSourceLibDm> CreateSource(AttributeSourceLibDm source);
    }
}