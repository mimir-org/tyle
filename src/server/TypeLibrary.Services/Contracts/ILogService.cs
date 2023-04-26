using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Services.Contracts;

public interface ILogService
{
    /// <summary>
    /// Get all logs
    /// </summary>
    /// <returns>A collection of all logs</returns>
    IEnumerable<LogLibCm> Get();

    /// <summary>
    /// Create a log entry
    /// </summary>
    /// <param name="logObject"></param>
    /// <param name="logType"></param>
    /// <param name="logTypeValue"></param>
    /// <param name="createdBy"></param>
    /// <returns>Completed task</returns>
    Task CreateLog(ILogable logObject, LogType logType, string logTypeValue, string createdBy);

    /// <summary>
    /// Create log entries
    /// </summary>
    /// <param name="logObjects"></param>
    /// <param name="logType"></param>
    /// <param name="logTypeValue"></param>
    /// <param name="createdBy"></param>
    /// <returns>Completed task</returns>
    Task CreateLogs(IEnumerable<ILogable> logObjects, LogType logType, string logTypeValue, string createdBy);

    /// <summary>
    /// Find last log - state - from object id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="objectType"></param>
    /// <returns>Return the state from last log-entry</returns>
    /// <exception cref="MimirorgNotFoundException"></exception>
    Task<State> GetPreviousState(string id, string objectType);
}