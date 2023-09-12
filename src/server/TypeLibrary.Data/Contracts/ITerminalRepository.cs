using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts;

public interface ITerminalRepository
{
    /*/// <summary>
    /// Change the state of the terminal with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The terminal id</param>
    Task ChangeState(State state, string id);*/

    /// <summary>
    /// Get all terminals
    /// </summary>
    /// <returns>A collection of terminals</returns>
    IEnumerable<TerminalType> Get();

    /// <summary>
    /// Get terminal by id
    /// </summary>
    /// <param name="id">The terminal id</param>
    /// <returns>Terminal if found</returns>
    TerminalType Get(Guid id);

    /// <summary>
    /// Create a terminal in database
    /// </summary>
    /// <param name="terminal">The terminal to be created</param>
    /// <returns>The created terminal</returns>
    Task<TerminalType> Create(TerminalType terminal);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}