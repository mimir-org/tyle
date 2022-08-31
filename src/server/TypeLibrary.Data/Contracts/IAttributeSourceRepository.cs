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
        Task<List<AttributeSourceLibDm>> GetSources();
    }
}