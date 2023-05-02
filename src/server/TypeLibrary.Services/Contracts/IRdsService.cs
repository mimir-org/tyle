using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IRdsService
{
    /// <summary>
    /// Get all RDS object
    /// </summary>
    /// <returns>List of RDS objects</returns>
    ICollection<RdsLibCm> Get();

    /// <summary>
    /// Get a RDS object by id
    /// </summary>
    /// <param name="id">The id of the RDS</param>
    /// <returns>The RDS with the given id</returns>
    RdsLibCm Get(string id);

    /// <summary>
    /// Create a new RDS object
    /// </summary>
    /// <param name="rdsAm">The RDS that should be created</param>
    /// <returns>The created RDS</returns>
    Task<RdsLibCm> Create(RdsLibAm rdsAm);

    /// <summary>
    /// Update an existing RDS
    /// </summary>
    /// <param name="id">The id of the RDS that should be updated</param>
    /// <param name="rdsAm">The new RDS values</param>
    /// <returns>The updated RDS</returns>
    Task<RdsLibCm> Update(string id, RdsLibAm rdsAm);

    /// <summary>
    /// Change RDS state
    /// </summary>
    /// <param name="id">The RDS id that should change state</param>
    /// <param name="state">The new RDS state</param>
    /// <returns>RDS with updated state</returns>
    Task<ApprovalDataCm> ChangeState(string id, State state);

    /// <summary>
    /// Initializes the database with RDS
    /// </summary>
    Task Initialize();
}