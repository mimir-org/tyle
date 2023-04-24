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
using TypeLibrary.Services.Constants;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class QuantityDatumService : IQuantityDatumService
{
    private readonly IMapper _mapper;
    private readonly IEfQuantityDatumRepository _quantityDatumRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;

    public QuantityDatumService(IMapper mapper, IEfQuantityDatumRepository quantityDatumRepository, ITimedHookService hookService, ILogService logService)
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
    public QuantityDatumLibCm Get(string id)
    {
        var quantityDatum = _quantityDatumRepository.Get(id);
        if (quantityDatum == null)
            throw new MimirorgNotFoundException($"Quantity datum with id {id} not found.");

        var data = _mapper.Map<QuantityDatumLibCm>(quantityDatum);
        return data;
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
    public async Task<QuantityDatumLibCm> Create(QuantityDatumLibAm quantityDatumAm, bool createdBySync = false)
    {
        if (quantityDatumAm == null)
            throw new ArgumentNullException(nameof(quantityDatumAm));

        var dm = _mapper.Map<QuantityDatumLibDm>(quantityDatumAm);

        if (createdBySync)
        {
            dm.CreatedBy = CreatedByConstants.Synchronization;
            dm.State = State.ApprovedGlobal;
        }
        else
        {
            dm.State = State.Draft;
        }
        
        var createdQuantityDatum = await _quantityDatumRepository.Create(dm);
        _quantityDatumRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdQuantityDatum, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);

        return _mapper.Map<QuantityDatumLibCm>(createdQuantityDatum);
    }

    /// <inheritdoc />
    public async Task<QuantityDatumLibCm> Update(string id, QuantityDatumLibAm quantityDatumAm)
    {
        var validation = quantityDatumAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Quantity datum is not valid.", validation);

        var quantityDatumToUpdate = _quantityDatumRepository.Get(id);

        if (quantityDatumToUpdate == null)
        {
            validation = new Validation(new List<string> { nameof(QuantityDatumLibAm.Name) },
                $"Quantity datum with name {quantityDatumAm.Name} and id {id} does not exist.");
            throw new MimirorgBadRequestException("Quantity datum does not exist or is flagged as deleted. Update is not possible.", validation);
        }

        quantityDatumToUpdate.Description = quantityDatumAm.Description;

        _quantityDatumRepository.Update(quantityDatumToUpdate);
        await _quantityDatumRepository.SaveAsync();

        _quantityDatumRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);

        return Get(quantityDatumToUpdate.Id);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state)
    {
        var dm = _quantityDatumRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Quantity datum with id {id} not found.");

        await _quantityDatumRepository.ChangeState(state, dm.Id);
        await _logService.CreateLog(dm, LogType.State, state.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);

        return new ApprovalDataCm
        {
            Id = id,
            State = state

        };
    }

    /// <inheritdoc />
    public int GetCompanyId(string id)
    {
        return _quantityDatumRepository.HasCompany(id);
    }
}