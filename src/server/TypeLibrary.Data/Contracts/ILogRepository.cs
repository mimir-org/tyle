using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface ILogRepository
{
    /// <summary>
    /// Get all logs
    /// </summary>
    /// <returns>A collection of logs</returns>
    IEnumerable<LogLibDm> Get();

    /// <summary>
    /// Create a log entry
    /// </summary>
    /// <param name="logDms">The log entry to be created</param>
    Task Create(ICollection<LogLibDm> logDms);
}