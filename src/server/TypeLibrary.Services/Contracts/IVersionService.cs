using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;

namespace TypeLibrary.Services.Contracts
{
    public interface IVersionService
    {
        /// <summary>
        /// Method will find and return the latest version.
        /// </summary>
        /// <typeparam name="T">NodeLibDm or TerminalLibDm</typeparam>
        /// <param name="obj">NodeLibDm or TerminalLibDm</param>
        /// <returns>Latest version of NodeLibDm</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        Task<T> GetLatestVersion<T>(T obj) where T : class;
    }
}