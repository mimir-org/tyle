using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class UnitService : IUnitService
{
    private readonly IMapper _mapper;
    private readonly IEfUnitRepository _unitRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly IHttpContextAccessor _contextAccessor;

    public UnitService(IMapper mapper, IEfUnitRepository unitRepository, ITimedHookService hookService, ILogService logService, IHttpContextAccessor contextAccessor)
    {
        _mapper = mapper;
        _unitRepository = unitRepository;
        _hookService = hookService;
        _logService = logService;
        _contextAccessor = contextAccessor;
    }

    /// <inheritdoc />
    public IEnumerable<UnitLibCm> Get()
    {
        var dataList = _unitRepository.Get().ExcludeDeleted().ToList();

        if (dataList == null)
            throw new MimirorgNotFoundException("No units were found.");

        return !dataList.Any() ? new List<UnitLibCm>() : _mapper.Map<List<UnitLibCm>>(dataList);
    }

    /// <inheritdoc />
    public UnitLibCm Get(string id)
    {
        var unit = _unitRepository.Get(id);
        if (unit == null)
            throw new MimirorgNotFoundException($"Unit with id {id} not found.");

        var data = _mapper.Map<UnitLibCm>(unit);
        return data;
    }

    /// <inheritdoc />
    public async Task<UnitLibCm> Create(UnitLibAm unitAm, string createdBy = null)
    {
        if (unitAm == null)
            throw new ArgumentNullException(nameof(unitAm));

        var dm = _mapper.Map<UnitLibDm>(unitAm);

        if (!string.IsNullOrEmpty(createdBy))
        {
            dm.CreatedBy = createdBy;
            dm.State = State.Approved;
        }
        else
        {
            dm.State = State.Draft;
        }

        var createdUnit = await _unitRepository.Create(dm);
        _unitRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdUnit, LogType.Create, createdUnit.State.ToString(), createdUnit.CreatedBy);
        _hookService.HookQueue.Enqueue(CacheKey.Unit);

        return _mapper.Map<UnitLibCm>(createdUnit);
    }

    /// <inheritdoc />
    public async Task<UnitLibCm> Update(string id, UnitLibAm unitAm)
    {
        var validation = unitAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Unit is not valid.", validation);

        var unitToUpdate = _unitRepository.Get(id);

        if (unitToUpdate == null)
        {
            validation = new Validation(new List<string> { nameof(UnitLibAm.Name) },
                $"Unit with name {unitAm.Name} and id {id} does not exist.");
            throw new MimirorgBadRequestException("Unit does not exist or is flagged as deleted. Update is not possible.", validation);
        }

        if (unitToUpdate.State != State.Approved)
        {
            unitToUpdate.Name = unitAm.Name;
            unitToUpdate.TypeReference = unitAm.TypeReference;
            unitToUpdate.Symbol = unitAm.Symbol;

            unitToUpdate.State = State.Draft;
        }
        unitToUpdate.Description = unitAm.Description;

        _unitRepository.Update(unitToUpdate);
        await _unitRepository.SaveAsync();
        var updatedBy = !string.IsNullOrWhiteSpace(_contextAccessor.GetName()) ? _contextAccessor.GetName() : CreatedBy.Unknown;
        await _logService.CreateLog(unitToUpdate, LogType.Update, unitToUpdate.State.ToString(), updatedBy);

        _unitRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Unit);

        return Get(unitToUpdate.Id);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state)
    {
        var dm = _unitRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Unit with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgBadRequestException(
                $"State change on approved unit with id {id} is not allowed.");

        await _unitRepository.ChangeState(state, dm.Id);

        await _logService.CreateLog(
            dm,
            LogType.State,
            state.ToString(),
            !string.IsNullOrWhiteSpace(_contextAccessor.GetName()) ? _contextAccessor.GetName() : CreatedBy.Unknown);

        _hookService.HookQueue.Enqueue(CacheKey.Unit);

        return new ApprovalDataCm
        {
            Id = id,
            State = state

        };
    }
}