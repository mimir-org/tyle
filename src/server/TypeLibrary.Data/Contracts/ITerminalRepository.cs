using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITerminalRepository
    {
        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The terminal id</param>
        /// <returns>The company id of given terminal</returns>
        Task<int> HasCompany(int id);

        /// <summary>
        /// Change the state of the terminal on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of terminal id's</param>
        /// <returns>The number of terminals in given state</returns>
        Task<int> ChangeState(State state, ICollection<int> ids);

        /// <summary>
        /// Change all parent id's on terminals from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old terminal parent id</param>
        /// <param name="newId">New terminal parent id</param>
        /// <returns>The number of terminal with the new parent id</returns>
        Task<int> ChangeParentId(int oldId, int newId);

        /// <summary>
        /// Check if terminal exists
        /// </summary>
        /// <param name="id">The id of the terminal</param>
        /// <returns>True if terminal exist</returns>
        Task<bool> Exist(int id);

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
        Task<TerminalLibDm> Get(int id);

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
}