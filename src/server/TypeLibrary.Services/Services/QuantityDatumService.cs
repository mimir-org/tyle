using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
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

public class QuantityDatumService : IQuantityDatumService
{
    private readonly IMapper _mapper;
    private readonly IEfQuantityDatumRepository _quantityDatumRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmailService _emailService;

    public QuantityDatumService(IMapper mapper, IEfQuantityDatumRepository quantityDatumRepository, ITimedHookService hookService, ILogService logService, IHttpContextAccessor contextAccessor, IEmailService emailService)
    {
        _mapper = mapper;
        _quantityDatumRepository = quantityDatumRepository;
        _hookService = hookService;
        _logService = logService;
        _contextAccessor = contextAccessor;
        _emailService = emailService;
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibCm> Get()
    {
        var dataSet = _quantityDatumRepository.Get()?.ToList();

        if (dataSet == null || !dataSet.Any())
            return new List<QuantityDatumLibCm>();

        return _mapper.Map<List<QuantityDatumLibCm>>(dataSet);
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
    public async Task<QuantityDatumLibCm> Create(QuantityDatumLibAm quantityDatumAm, string createdBy = null)
    {
        if (quantityDatumAm == null)
            throw new ArgumentNullException(nameof(quantityDatumAm));

        var validation = quantityDatumAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Quantity datum is not valid.", validation);

        var dm = _mapper.Map<QuantityDatumLibDm>(quantityDatumAm);

        if (!string.IsNullOrEmpty(createdBy))
        {
            dm.CreatedBy = createdBy;
            dm.State = State.Approved;
        }
        else
        {
            dm.State = State.Draft;
        }

        var createdQuantityDatum = await _quantityDatumRepository.Create(dm);
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);
        _quantityDatumRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdQuantityDatum, LogType.Create, createdQuantityDatum.State.ToString(), createdQuantityDatum.CreatedBy);

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
            throw new MimirorgNotFoundException("Quantity datum not found. Update is not possible.");

        if (quantityDatumToUpdate.State != State.Approved && quantityDatumToUpdate.State != State.Draft)
            throw new MimirorgInvalidOperationException("Update can only be performed on quantity datum drafts or approved quantity datums.");

        if (quantityDatumToUpdate.State != State.Approved)
        {
            quantityDatumToUpdate.Name = quantityDatumAm.Name;
            quantityDatumToUpdate.TypeReference = quantityDatumAm.TypeReference;
            quantityDatumToUpdate.QuantityDatumType = quantityDatumAm.QuantityDatumType;
            quantityDatumToUpdate.State = State.Draft;
        }

        quantityDatumToUpdate.Description = quantityDatumAm.Description;

        _quantityDatumRepository.Update(quantityDatumToUpdate);
        await _quantityDatumRepository.SaveAsync();
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);
        _quantityDatumRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(quantityDatumToUpdate, LogType.Update, quantityDatumToUpdate.State.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        return Get(quantityDatumToUpdate.Id);
    }

    /// <inheritdoc />
    public async Task Delete(string id)
    {
        var dm = _quantityDatumRepository.Get(id) ?? throw new MimirorgNotFoundException($"Quantity datum with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"Can't delete approved quantity datum with id {id}.");

        await _quantityDatumRepository.Delete(id);
        await _quantityDatumRepository.SaveAsync();
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
    {
        var dm = _quantityDatumRepository.Get(id) ?? throw new MimirorgNotFoundException($"Quantity datum with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State '{state}' is not allowed for object {dm.Name} with id {dm.Id} since current state is {dm.State}");

        await _quantityDatumRepository.ChangeState(state, dm.Id);
        _hookService.HookQueue.Enqueue(CacheKey.QuantityDatum);
        await _logService.CreateLog(dm, LogType.State, state.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        if (sendStateEmail)
            await _emailService.SendObjectStateEmail(dm.Id, state, dm.Name, ObjectTypeName.QuantityDatum);

        return new ApprovalDataCm { Id = dm.Id, State = state };
    }
}