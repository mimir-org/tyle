using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface ITerminalService
{
    /// <summary>
    /// Get the latest version of a terminal based on given id
    /// </summary>
    /// <param name="id">The id of the terminal</param>
    /// <returns>The latest version of the terminal of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no terminal with the given id, and that terminal is at the latest version.</exception>
    TerminalLibCm GetLatestVersion(int id);

    /// <summary>
    /// Get the latest terminal versions
    /// </summary>
    /// <returns>A collection of terminals</returns>
    IEnumerable<TerminalLibCm> GetLatestVersions();

    /// <summary>
    /// Create a new terminal
    /// </summary>
    /// <param name="terminal">The terminal that should be created</param>
    /// <returns></returns>
    /// <exception cref="MimirorgBadRequestException">Throws if terminal is not valid</exception>
    /// <exception cref="MimirorgDuplicateException">Throws if terminal already exist</exception>
    /// <remarks>Remember that creating a new terminal could be creating a new version of existing terminal.
    /// They will have the same first version id, but have different version and id.</remarks>
    Task<TerminalLibCm> Create(TerminalLibAm terminal);

    /// <summary>
    /// Update a terminal if the data is allowed to be changed.
    /// </summary>
    /// <param name="id">The id of the terminal to update</param>
    /// <param name="terminalAm">The terminal to update</param>
    /// <returns>The updated terminal</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if the terminal does not exist,
    /// if it is not valid or there are not allowed changes.</exception>
    /// <remarks>ParentId to old references will also be updated.</remarks>
    Task<TerminalLibCm> Update(int id, TerminalLibAm terminalAm);

    /// <summary>
    /// Change terminal state
    /// </summary>
    /// <param name="id">The terminal id that should change the state</param>
    /// <param name="state">The new terminal state</param>
    /// <returns>Terminal with updated state</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the terminal does not exist on latest version</exception>
    Task<TerminalLibCm> ChangeState(int id, State state);

    /// <summary>
    /// Get terminal existing company id for terminal by id
    /// </summary>
    /// <param name="id">The terminal id</param>
    /// <returns>Company id for terminal</returns>
    Task<int> GetCompanyId(int id);
}