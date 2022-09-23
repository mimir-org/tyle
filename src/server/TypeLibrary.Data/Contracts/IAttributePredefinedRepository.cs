using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributePredefinedRepository
    {
        /// <summary>
        /// Get all predefined attributes
        /// </summary>
        /// <returns>A collection of predefined attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        IEnumerable<AttributePredefinedLibDm> GetPredefined();

        /// <summary>
        /// Create a predefined attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <param name="state"></param>
        /// <returns>An attribute</returns>
        Task<AttributePredefinedLibDm> CreatePredefined(AttributePredefinedLibDm attribute, State state);
    }
}