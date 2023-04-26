using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class RdsService : IRdsService
{
    private readonly IMapper _mapper;
    private readonly IEfRdsRepository _rdsRepository;
    private readonly ILogService _logService;
    private readonly ITimedHookService _hookService;

    public RdsService(IMapper mapper, IEfRdsRepository rdsRepository, ILogService logService, ITimedHookService hookService)
    {
        _mapper = mapper;
        _rdsRepository = rdsRepository;
        _logService = logService;
        _hookService = hookService;
    }

    public ICollection<RdsLibCm> Get()
    {
        var dataList = _rdsRepository.Get().ToList().OrderBy(x => x.RdsCode.Length).ThenBy(x => x.RdsCode, StringComparer.InvariantCultureIgnoreCase);

        if (dataList == null)
            throw new MimirorgNotFoundException("No RDS objects were found.");

        return !dataList.Any() ? new List<RdsLibCm>() : _mapper.Map<List<RdsLibCm>>(dataList);
    }

    /// <inheritdoc />
    public RdsLibCm Get(string id)
    {
        var rds = _rdsRepository.Get(id);
        if (rds == null)
            throw new MimirorgNotFoundException($"RDS with id {id} not found.");

        var data = _mapper.Map<RdsLibCm>(rds);
        return data;
    }

    /// <inheritdoc />
    public async Task<RdsLibCm> Create(RdsLibAm rdsAm)
    {
        if (rdsAm == null)
            throw new ArgumentNullException(nameof(rdsAm));

        var dm = _mapper.Map<RdsLibDm>(rdsAm);

        dm.State = State.Draft;

        var createdRds = await _rdsRepository.Create(dm);
        _rdsRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdRds, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.Rds);

        return _mapper.Map<RdsLibCm>(createdRds);
    }

    /// <inheritdoc />
    public async Task<RdsLibCm> Update(string id, RdsLibAm rdsAm)
    {
        var validation = rdsAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("RDS is not valid.", validation);

        var rdsToUpdate = _rdsRepository.Get(id);

        if (rdsToUpdate == null)
        {
            validation = new Validation(new List<string> { nameof(RdsLibAm.Name) },
                $"RDS with name {rdsAm.Name} and id {id} does not exist.");
            throw new MimirorgBadRequestException("RDS does not exist or is flagged as deleted. Update is not possible.", validation);
        }

        rdsToUpdate.Description = rdsAm.Description;

        _rdsRepository.Update(rdsToUpdate);
        await _rdsRepository.SaveAsync();

        _rdsRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Rds);

        return Get(rdsToUpdate.Id);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state)
    {
        var dm = _rdsRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"RDS with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException(
                $"State change on approved RDS with id {id} is not allowed.");

        await _rdsRepository.ChangeState(state, dm.Id);
        await _logService.CreateLog(dm, LogType.State, state.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.Rds);

        return new ApprovalDataCm
        {
            Id = id,
            State = state

        };
    }

    /// <inheritdoc />
    public async Task Initialize()
    {
        await _rdsRepository.InitializeDb();
    }
}