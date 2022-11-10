using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IPurposeReferenceRepository
    {
        /// <summary>
        /// Get all purposes
        /// </summary>
        /// <returns>List of purpose sorted by name></returns>
        Task<List<PurposeLibDm>> Get();
    }
}