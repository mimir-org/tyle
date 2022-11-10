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
        /// <returns>List of purpose></returns>
        Task<List<PurposeLibDm>> Get();
    }
}