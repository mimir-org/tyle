using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class QuantityDatumService : IQuantityDatumService
{
    private readonly IMapper _mapper;
    private readonly IQuantityDatumRepository _quantityDatumRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;

    public QuantityDatumService(IMapper mapper, IQuantityDatumRepository quantityDatumRepository, ITimedHookService hookService, ILogService logService)
    {
        _mapper = mapper;
        _quantityDatumRepository = quantityDatumRepository;
        _hookService = hookService;
        _logService = logService;
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibCm> Get()
    {
        var dataSet = _quantityDatumRepository.Get().ToList();

        if (dataSet == null)
            throw new MimirorgNotFoundException("No quantity datums were found.");

        return !dataSet.Any() ? new List<QuantityDatumLibCm>() : _mapper.Map<List<QuantityDatumLibCm>>(dataSet);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibCm> GetQuantityDatumRangeSpecifying()
    {
        var dataSet = _quantityDatumRepository.GetQuantityDatumRangeSpecifying();
        var dataCmList = _mapper.Map<List<QuantityDatumLibCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibCm> GetQuantityDatumSpecifiedScope()
    {
        var dataSet = _quantityDatumRepository.GetQuantityDatumSpecifiedScope();
        var dataCmList = _mapper.Map<List<QuantityDatumLibCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibCm> GetQuantityDatumSpecifiedProvenance()
    {
        var dataSet = _quantityDatumRepository.GetQuantityDatumSpecifiedProvenance();
        var dataCmList = _mapper.Map<List<QuantityDatumLibCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibCm> GetQuantityDatumRegularitySpecified()
    {
        var dataSet = _quantityDatumRepository.GetQuantityDatumRegularitySpecified();
        var dataCmList = _mapper.Map<List<QuantityDatumLibCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }

    /// <inheritdoc />
    public async Task<QuantityDatumLibCm> Create(QuantityDatumLibAm quantityDatumAm)
    {
        if (quantityDatumAm == null)
            throw new ArgumentNullException(nameof(quantityDatumAm));

        var dm = _mapper.Map<QuantityDatumLibDm>(quantityDatumAm);

        dm.State = State.Draft;

        var createdQuantityDatum = await _quantityDatumRepository.Create(dm);
        _quantityDatumRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdQuantityDatum, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);

        return _mapper.Map<QuantityDatumLibCm>(createdQuantityDatum);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state)
    {
        var dm = _quantityDatumRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Quantity datum with id {id} not found.");

        await _quantityDatumRepository.ChangeState(state, new List<int> { dm.Id });
        await _logService.CreateLog(dm, LogType.State, state.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);

        return new ApprovalDataCm
        {
            Id = id.ToString(),
            State = state

        };
    }

    /// <inheritdoc />
    public async Task<int> GetCompanyId(string id)
    {
        return await _quantityDatumRepository.HasCompany(id);
    }
}