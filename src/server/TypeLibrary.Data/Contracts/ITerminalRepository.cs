using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface ITerminalRepository
{
    /// <summary>
    /// Change the state of the terminal with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The terminal id</param>
    Task ChangeState(State state, string id);

    /// <summary>
    /// Change the state of the terminal on all listed ids
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of terminal ids</param>
    /// <returns>The number of terminals with changed state</returns>
    Task<int> ChangeState(State state, ICollection<string> ids);

    /// <summary>
    /// Check if terminal exists
    /// </summary>
    /// <param name="id">The id of the terminal</param>
    /// <returns>True if terminal exist</returns>
    Task<bool> Exist(string id);

    /// <summary>
    /// Get all terminals
    /// </summary>
    /// <returns>A collection of terminals</returns>
    IEnumerable<TerminalLibDm> Get();

    /// <summary>
    /// Get terminal by id
    /// </summary>
    /// <param name="id">The terminal id</param>
    /// <returns>Terminal if found</returns>
    TerminalLibDm Get(string id);

    /// <summary>
    /// Create a terminal in database
    /// </summary>
    /// <param name="terminal">The terminal to be created</param>
    /// <returns>The created terminal</returns>
    Task<TerminalLibDm> Create(TerminalLibDm terminal);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}