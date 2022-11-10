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
        /// <returns>List of purposes sorted by name></returns>
        Task<ICollection<PurposeLibCm>> Get();
    }
}