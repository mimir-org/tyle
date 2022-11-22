using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IPurposeService
    {
        /// <summary>
        /// Get all purposes
        /// </summary>
        /// <returns>List of purposes></returns>
        Task<ICollection<PurposeLibCm>> Get();
    }
}