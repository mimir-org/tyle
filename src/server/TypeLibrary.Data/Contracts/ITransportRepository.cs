using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITransportRepository
    {
        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The transport id</param>
        /// <returns>The company id of given transport</returns>
        Task<int> HasCompany(string id);

        /// <summary>
        /// Change the state of the transport on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of transport id's</param>
        /// <returns>The number of transport in given state</returns>
        Task<int> ChangeState(State state, ICollection<string> ids);

        /// <summary>
        /// Change all parent id's on transport from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old transport parent id</param>
        /// <param name="newId">New transport parent id</param>
        /// <returns>The number of transport with the new parent id</returns>
        Task<int> ChangeParentId(string oldId, string newId);

        /// <summary>
        /// Check if transport exists
        /// </summary>
        /// <param name="id">The id of the transport</param>
        /// <returns>True if transport exist</returns>
        Task<bool> Exist(string id);

        /// <summary>
        /// Get all transport
        /// </summary>
        /// <returns>A collection of transport</returns>
        IEnumerable<TransportLibDm> Get();

        /// <summary>
        /// Get transport by id
        /// </summary>
        /// <param name="id">The transport id</param>
        /// <returns>Transport if found</returns>
        Task<TransportLibDm> Get(string id);

        /// <summary>
        /// Create a transport
        /// </summary>
        /// <param name="transportDm">The transport to be created</param>
        /// <returns>The created transport</returns>
        Task<TransportLibDm> Create(TransportLibDm transportDm);

        /// <summary>
        /// Clear all entity framework change trackers
        /// </summary>
        void ClearAllChangeTrackers();
    }
}