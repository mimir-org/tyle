using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalService
    {
        /// <summary>
        /// Get the latest version of a terminal based on given id
        /// </summary>
        /// <param name="id">The id of the terminal</param>
        /// <param name="includeDeleted">Include deleted versions</param>
        /// <returns>The latest version of the terminal of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no terminal with the given id, and that terminal is at the latest version.</exception>
        TerminalLibCm GetLatestVersion(string id, bool includeDeleted = false);

        /// <summary>
        /// Get the latest terminal versions
        /// </summary>
        /// <param name="includeDeleted">Include deleted versions</param>
        /// <returns>A collection of terminals</returns>
        IEnumerable<TerminalLibCm> GetLatestVersions(bool includeDeleted = false);

        /// <summary>
        /// Create a new terminal
        /// </summary>
        /// <param name="terminal">The terminal that should be created</param>
        /// <param name="resetVersion">Would you reset version and first version id?</param>
        /// <returns></returns>
        /// <exception cref="MimirorgBadRequestException">Throws if terminal is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if terminal already exist</exception>
        /// <remarks>Remember that creating a new terminal could be creating a new version of existing terminal.
        /// They will have the same first version id, but have different version and id.</remarks>
        Task<TerminalLibCm> Create(TerminalLibAm terminal, bool resetVersion);


        Task<TerminalLibCm> Update(TerminalLibAm terminal, bool includeDeleted = false);
        Task<TerminalLibCm> UpdateState(string id, State state);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string terminalId, int companyId);
    }
}