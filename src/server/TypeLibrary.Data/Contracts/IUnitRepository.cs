using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IUnitRepository
    {
        /// <summary>
        /// Get all units
        /// </summary>
        /// <returns>A collection of units</returns>
        /// <remarks>Only units that is not deleted will be returned</remarks>
        IEnumerable<UnitLibDm> GetUnits();

        /// <summary>
        /// Creates a new unit
        /// </summary>
        /// <param name="unit">The unit that should be created</param>
        /// <returns>An unit</returns>
        Task<UnitLibDm> CreateUnit(UnitLibDm unit);
    }
}