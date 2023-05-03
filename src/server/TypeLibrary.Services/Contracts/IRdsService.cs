using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
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
    /// <exception cref="MimirorgNotFoundException">Throws if there is no RDS with the given id.</exception>
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
    /// <exception cref="MimirorgNotFoundException">Throws if there is no RDS with the given id.</exception>
    /// <exception cref="MimirorgBadRequestException">Throws if the RDS is not valid.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the RDS is not a draft or approved.</exception>
    Task<RdsLibCm> Update(string id, RdsLibAm rdsAm);

    /// <summary>
    /// Change RDS state
    /// </summary>
    /// <param name="id">The id of the RDS that should change state</param>
    /// <param name="state">The new RDS state</param>
    /// <returns>An approval data object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the RDS does not exist</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the RDS is already approved.</exception>
    Task<ApprovalDataCm> ChangeState(string id, State state);

    /// <summary>
    /// Initializes the database with RDS
    /// </summary>
    Task Initialize();
}