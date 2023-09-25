/*using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts;

public interface ILogRepository
{
    /// <summary>
    /// Get all logs
    /// </summary>
    /// <returns>A collection of logs</returns>
    IEnumerable<LogLibDm> Get();

    /// <summary>
    /// Get all logs for a specific object
    /// </summary>
    /// <param name="objectId">The id of the object to get logs for</param>
    /// <returns></returns>
    IEnumerable<LogLibDm> Get(string objectId);

    /// <summary>
    /// Create a log entry
    /// </summary>
    /// <param name="logDms">The log entry to be created</param>
    Task Create(ICollection<LogLibDm> logDms);
}*/