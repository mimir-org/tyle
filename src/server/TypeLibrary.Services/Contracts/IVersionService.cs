using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;

namespace TypeLibrary.Services.Contracts;

public interface IVersionService
{
    /// <summary>
    /// Method will find and return the latest version.
    /// </summary>
    /// <typeparam name="T">AspectObjectLibDm or TerminalLibDm</typeparam>
    /// <param name="obj">AspectObjectLibDm or TerminalLibDm</param>
    /// <returns>Latest version of AspectObjectLibDm</returns>
    /// <exception cref="MimirorgBadRequestException"></exception>
    Task<T> GetLatestVersion<T>(T obj) where T : class;
}