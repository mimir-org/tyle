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
        Task<List<UnitLibDm>> Get();

        /// <summary>
        /// Create an unit
        /// </summary>
        /// <param name="unit">The unit to be created</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        Task<UnitLibDm> CreateUnit(UnitLibDm unit);
    }
}