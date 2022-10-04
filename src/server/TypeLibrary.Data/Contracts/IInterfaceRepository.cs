using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IInterfaceRepository
    {
        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The interface id</param>
        /// <returns>The company id of given interface</returns>
        Task<int> HasCompany(string id);

        /// <summary>
        /// Change the state of the interface on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of interface id's</param>
        /// <returns>The number of interfaces in given state</returns>
        Task<int> ChangeState(State state, ICollection<string> ids);

        /// <summary>
        /// Change all parent id's on interface from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old interface parent id</param>
        /// <param name="newId">New interface parent id</param>
        /// <returns>The number of interface with the new parent id</returns>
        Task<int> ChangeParentId(string oldId, string newId);

        /// <summary>
        /// Check if interface exists
        /// </summary>
        /// <param name="id">The id of the interface</param>
        /// <returns>True if interface exist</returns>
        Task<bool> Exist(string id);

        /// <summary>
        /// Get all interface
        /// </summary>
        /// <returns>A collection of interface</returns>
        IEnumerable<InterfaceLibDm> Get();

        /// <summary>
        /// Get interface by id
        /// </summary>
        /// <param name="id">The interface id</param>
        /// <returns>Interface if found</returns>
        Task<InterfaceLibDm> Get(string id);

        /// <summary>
        /// Create an interface
        /// </summary>
        /// <param name="interfaceDm">The interface to be created</param>
        /// <returns>The created interfaces</returns>
        Task<InterfaceLibDm> Create(InterfaceLibDm interfaceDm);

        /// <summary>
        /// Clear all entity framework change trackers
        /// </summary>
        void ClearAllChangeTrackers();
    }
}