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
        IEnumerable<AttributeConditionLibDm> GetConditions();

        /// <summary>
        /// Creates a new attribute condition
        /// </summary>
        /// <param name="format">The attribute condition that should be created</param>
        /// <returns>An attribute condition</returns>
        Task<AttributeConditionLibDm> CreateCondition(AttributeConditionLibDm format);
    }
}