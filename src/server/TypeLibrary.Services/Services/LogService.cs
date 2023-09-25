/*using AutoMapper;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
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

    public List<LogLibCm> Get(string objectId)
    {
        var dms = _logRepository.Get(objectId);

        return _mapper.Map<List<LogLibCm>>(dms);
    }

    /// <summary>
    /// Create a log entry
    /// </summary>
    /// <param name="logObject"></param>
    /// <param name="logType"></param>
    /// <param name="logTypeValue"></param>
    /// <param name="createdBy"></param>
    /// <returns>Completed task</returns>
    public async Task CreateLog(ILogable logObject, LogType logType, string logTypeValue, string createdBy)
    {
        var logDm = logObject.CreateLog(logType, logTypeValue, createdBy);
        await _logRepository.Create(new List<LogLibDm> { logDm });
    }

    /// <summary>
    /// Create log entries
    /// </summary>
    /// <param name="logObjects"></param>
    /// <param name="logType"></param>
    /// <param name="logTypeValue"></param>
    /// <param name="createdBy"></param>
    /// <returns>Completed task</returns>
    public async Task CreateLogs(IEnumerable<ILogable> logObjects, LogType logType, string logTypeValue, string createdBy)
    {
        var logDms = logObjects.Select(x => x.CreateLog(logType, logTypeValue, createdBy)).ToList();
        await _logRepository.Create(logDms);
    }
}*/