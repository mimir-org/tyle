using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IRdsRepository
{
    /// <summary>
    /// Change the state of the RDS with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The RDS id</param>
    Task ChangeState(State state, string id);

    /// <summary>
    /// Get all RDS
    /// </summary>
    /// <returns>A collection of RDS</returns>
    IEnumerable<RdsLibDm> Get();

    /// <summary>
    /// Get a specific RDS by id
    /// </summary>
    /// <param name="id">The id of the RDS to get</param>
    /// <returns>The RDS with the given id</returns>
    RdsLibDm Get(string id);

    /// <summary>
    /// Create a RDS object
    /// </summary>
    /// <param name="rds">The RDS to be created</param>
    /// <returns>The created RDS</returns>
    Task<RdsLibDm> Create(RdsLibDm rds);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();

    /// <summary>
    /// Initializes the database with RDS objects
    /// </summary>
    Task InitializeDb();
}