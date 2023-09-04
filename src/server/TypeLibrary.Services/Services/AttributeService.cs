using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class AttributeService : IAttributeService
{
    private readonly IMapper _mapper;
    private readonly IEfAttributeRepository _attributeRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmailService _emailService;

    public AttributeService(IMapper mapper, IEfAttributeRepository attributeRepository, ITimedHookService hookService, ILogService logService, IHttpContextAccessor contextAccessor, IEmailService emailService)
    {
        _mapper = mapper;
        _attributeRepository = attributeRepository;
        _hookService = hookService;
        _logService = logService;
        _contextAccessor = contextAccessor;
        _emailService = emailService;
    }

    /// <inheritdoc />
    public IEnumerable<AttributeLibCm> Get()
    {
        var dataSet = _attributeRepository.Get()?.ToList();

        if (dataSet == null || !dataSet.Any())
            return new List<AttributeLibCm>();

        return _mapper.Map<List<AttributeLibCm>>(dataSet);
    }

    /// <inheritdoc />
    public AttributeLibCm Get(Guid id)
    {
        var dm = _attributeRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

        return _mapper.Map<AttributeLibCm>(dm);
    }

    /// <inheritdoc />
    public async Task<AttributeLibCm> Create(AttributeLibAm attributeAm, string createdBy = null)
    {
        if (attributeAm == null)
            throw new ArgumentNullException(nameof(attributeAm));

        var validation = attributeAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Attribute is not valid.", validation);

        var dm = _mapper.Map<AttributeType>(attributeAm);

        if (!string.IsNullOrEmpty(createdBy))
        {
            dm.CreatedBy = createdBy;
            //dm.State = State.Approved;
        }
        else
        {
            //dm.State = State.Draft;
        }

        /*foreach (var attributeUnit in dm.AttributeUnits)
        {
            attributeUnit.AttributeId = dm.Id;
        }*/

        var createdAttribute = await _attributeRepository.Create(dm);
        _hookService.HookQueue.Enqueue(CacheKey.Attribute);
        _attributeRepository.ClearAllChangeTrackers();
        //await _logService.CreateLog(createdAttribute, LogType.Create, createdAttribute?.State.ToString(), createdAttribute?.CreatedBy);

        return _mapper.Map<AttributeLibCm>(createdAttribute);
    }

    /*/// <inheritdoc />
    public async Task<AttributeLibCm> Update(Guid id, AttributeLibAm attributeAm)
    {
        var validation = attributeAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Attribute is not valid.", validation);

        var attributeToUpdate = _attributeRepository.FindBy(x => x.Id == id, false).Include(x => x.AttributeUnits).FirstOrDefault();

        if (attributeToUpdate == null)
            throw new MimirorgNotFoundException("Attribute not found. Update is not possible.");

        if (attributeToUpdate.State != State.Approved && attributeToUpdate.State != State.Draft)
            throw new MimirorgInvalidOperationException("Update can only be performed on attribute drafts or approved attributes.");

        if (attributeToUpdate.State != State.Approved)
        {
            attributeToUpdate.Name = attributeAm.Name;
            attributeToUpdate.TypeReference = attributeAm.TypeReference;
            attributeToUpdate.Description = attributeAm.Description;
            attributeToUpdate.AttributeUnits ??= new List<AttributeUnitLibDm>();

            var currentAttributeUnits = attributeToUpdate.AttributeUnits.ToHashSet();
            var newAttributeUnits = _mapper.Map<HashSet<AttributeUnitLibDm>>(attributeAm.AttributeUnits.ToHashSet());

            foreach (var attributeUnit in currentAttributeUnits.ExceptBy(newAttributeUnits.Select(x => x.UnitId + x.IsDefault), y => y.UnitId + y.IsDefault))
            {
                var attributeUnitDm = _attributeUnitRepository.FindBy(x => x.AttributeId == attributeToUpdate.Id && x.UnitId == attributeUnit.UnitId).FirstOrDefault();

                if (attributeUnitDm == null)
                    continue;

                await _attributeUnitRepository.Delete(attributeUnitDm.Id);
            }

            foreach (var attributeUnit in newAttributeUnits.ExceptBy(currentAttributeUnits.Select(x => x.UnitId + x.IsDefault), y => y.UnitId + y.IsDefault))
            {
                attributeUnit.AttributeId = attributeToUpdate.Id;
                attributeToUpdate.AttributeUnits.Add(attributeUnit);
            }

            attributeToUpdate.State = State.Draft;
        }
        else
        {
            attributeToUpdate.Description = attributeAm.Description;
        }

        await _attributeUnitRepository.SaveAsync();
        await _attributeRepository.SaveAsync();
        _hookService.HookQueue.Enqueue(CacheKey.Attribute);
        _attributeRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(attributeToUpdate, LogType.Update, attributeToUpdate.State.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        return Get(attributeToUpdate.Id);
    }*/

    /// <inheritdoc />
    public async Task Delete(Guid id)
    {
        var dm = _attributeRepository.Get(id) ?? throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

        /*if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"Can't delete approved attribute with id {id}.");*/

        await _attributeRepository.Delete(id);
        await _attributeRepository.SaveAsync();
    }

    /*/// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
    {
        var dm = _attributeRepository.Get(id) ?? throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State '{state}' is not allowed for object {dm.Name} with id {dm.Id} since current state is {dm.State}");

        if (state == State.Review)
        {
            foreach (var attributeUnit in dm.AttributeUnits)
            {
                var unit = _unitService.Get(attributeUnit.UnitId);

                if (unit.State == State.Approved)
                    continue;

                await _unitService.ChangeState(unit.Id, State.Review, true);
            }
        }
        else if (state == State.Approved && dm.AttributeUnits.Select(attributeUnit => _unitService.Get(attributeUnit.UnitId)).Any(unit => unit.State != State.Approved))
        {
            throw new MimirorgInvalidOperationException("Cannot approve attribute that uses unapproved units.");
        }

        await _attributeRepository.ChangeState(state, dm.Id);
        _hookService.HookQueue.Enqueue(CacheKey.Attribute);
        await _logService.CreateLog(dm, LogType.State, state.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        if (sendStateEmail)
            await _emailService.SendObjectStateEmail(dm.Id, state, dm.Name, ObjectTypeName.Attribute);

        return new ApprovalDataCm { Id = dm.Id, State = state };
    }

    /// <inheritdoc />
    public IEnumerable<AttributePredefinedLibCm> GetPredefined()
    {
        var attributes = _attributePredefinedRepository.GetPredefined().ToList()
            .OrderBy(x => x.Aspect).ThenBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).ToList();

        return _mapper.Map<List<AttributePredefinedLibCm>>(attributes);
    }

    /// <inheritdoc />
    public async Task CreatePredefined(List<AttributePredefinedLibAm> predefined)
    {
        if (predefined == null || !predefined.Any())
            return;

        var data = _mapper.Map<List<AttributePredefinedLibDm>>(predefined);
        var existing = _attributePredefinedRepository.GetPredefined().ToList();
        var notExisting = data.Exclude(existing, x => x.Key).ToList();

        if (!notExisting.Any())
            return;

        foreach (var attribute in notExisting)
        {
            attribute.CreatedBy = CreatedBy.Seeding;
            attribute.State = State.Approved;
            await _attributePredefinedRepository.CreatePredefined(attribute);
            await _logService.CreateLog(attribute, LogType.Create, attribute.State.ToString(), attribute.CreatedBy);
        }
    }*/
}