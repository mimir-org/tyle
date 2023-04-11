using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class LogService : ILogService
{
    private readonly IMapper _mapper;
    private readonly ILogRepository _logRepository;

    public LogService(IMapper mapper, ILogRepository logRepository)
    {
        _mapper = mapper;
        _logRepository = logRepository;
    }

    /// <summary>
    /// Get all logs
    /// </summary>
    /// <returns>A collection of all logs</returns>
    public IEnumerable<LogLibCm> Get()
    {
        var dms = _logRepository.Get().OrderBy(x => x.ObjectFirstVersionId).ThenBy(x => x.Created).ToList();

        return _mapper.Map<List<LogLibCm>>(dms);
    }

    /// <summary>
    /// Create a log entry
    /// </summary>
    /// <param name="logObject"></param>
    /// <param name="logType"></param>
    /// <param name="logTypeValue"></param>
    /// <param name="comment"></param>
    /// <returns>Completed task</returns>
    public async Task CreateLog(ILogable logObject, LogType logType, string logTypeValue, string comment = null)
    {
        var logAm = logObject.CreateLog(logType, logTypeValue, comment);

        await _logRepository.Create(_mapper.Map<List<LogLibDm>>(new List<LogLibAm> { logAm }));
    }

    /// <summary>
    /// Create log entries
    /// </summary>
    /// <param name="logObjects"></param>
    /// <param name="logType"></param>
    /// <param name="logTypeValue"></param>
    /// <param name="comment"></param>
    /// <returns>Completed task</returns>
    public async Task CreateLogs(IEnumerable<ILogable> logObjects, LogType logType, string logTypeValue, string comment = null)
    {
        var logAms = logObjects.Select(x => x.CreateLog(logType, logTypeValue, comment)).ToList();

        await _logRepository.Create(_mapper.Map<List<LogLibDm>>(logAms));
    }

    /// <summary>
    /// Find last log - state - from object id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="objectType"></param>
    /// <returns>Return the state from last log-entry</returns>
    /// <exception cref="MimirorgNotFoundException"></exception>
    public Task<State> GetPreviousState(string id, string objectType)
    {
        var logEntry = _logRepository.Get()
            .Where(x =>
                x.ObjectId == id &&
                x.LogType == LogType.State &&
                x.ObjectType == objectType &&
                x.LogTypeValue != State.Delete.ToString() &&
                x.LogTypeValue != State.ApproveCompany.ToString() &&
                x.LogTypeValue != State.ApproveGlobal.ToString())
            .MaxBy(x => x.Id);

        if (logEntry == null)
            throw new MimirorgNotFoundException($"Can't find any log entry with id: {id}");

        if (!Enum.TryParse(logEntry.LogTypeValue, false, out State state))
            throw new MimirorgNotFoundException($"Can't convert log-entry to state for type with id: {id}");

        return Task.FromResult(state);
    }
}