using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeConditionRepository
    {
        /// <summary>
        /// Get all attribute conditions
        /// </summary>
        /// <returns>A collection of attribute conditions</returns>
        /// <remarks>Only conditions that is not deleted will be returned</remarks>
        Task<List<AttributeConditionLibDm>> GetConditions();
    }
}