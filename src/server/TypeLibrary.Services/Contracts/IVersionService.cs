using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;

namespace TypeLibrary.Services.Contracts
{
    public interface IVersionService
    {
        /// <summary>
        /// Method will find and return the latest version.
        /// </summary>
        /// <typeparam name="T">NodeLibDm, TransportLibDm or InterfaceLibDm</typeparam>
        /// <param name="obj">NodeLibDm, TransportLibDm or InterfaceLibDm</param>
        /// <returns>Latest version of NodeLibDm, TransportLibDm or InterfaceLibDm</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        Task<T> GetLatestVersion<T>(T obj) where T : class;
    }
}