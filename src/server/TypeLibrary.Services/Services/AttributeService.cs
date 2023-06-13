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
    private readonly IAttributePredefinedRepository _attributePredefinedRepository;
    private readonly IEfAttributeRepository _attributeRepository;
    private readonly IEfAttributeUnitRepository _attributeUnitRepository;
    private readonly IUnitService _unitService;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmailService _emailService;

    public AttributeService(IMapper mapper, IAttributePredefinedRepository attributePredefinedRepository, IEfAttributeRepository attributeRepository, IEfAttributeUnitRepository attributeUnitRepository, IUnitService unitService, ITimedHookService hookService, ILogService logService, IHttpContextAccessor contextAccessor, IEmailService emailService)
    {
        _mapper = mapper;
        _attributePredefinedRepository = attributePredefinedRepository;
        _attributeRepository = attributeRepository;
        _attributeUnitRepository = attributeUnitRepository;
        _unitService = unitService;
        _hookService = hookService;
        _logService = logService;
        _contextAccessor = contextAccessor;
        _emailService = emailService;
    }

    /// <inheritdoc />
    public IEnumerable<AttributeLibCm> Get()
    {
        var dataSet = _attributeRepository.Get()?.ExcludeDeleted().ToList();

        if (dataSet == null || !dataSet.Any())
            return new List<AttributeLibCm>();

        return _mapper.Map<List<AttributeLibCm>>(dataSet);
    }

    /// <inheritdoc />
    public AttributeLibCm Get(string id)
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

        var dm = _mapper.Map<AttributeLibDm>(attributeAm);

        if (!string.IsNullOrEmpty(createdBy))
        {
            dm.CreatedBy = createdBy;
            dm.State = State.Approved;
        }
        else
        {
            dm.State = State.Draft;
        }

        foreach (var attributeUnit in dm.AttributeUnits)
        {
            attributeUnit.AttributeId = dm.Id;
        }

        var createdAttribute = await _attributeRepository.Create(dm);
        _attributeRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdAttribute, LogType.Create, createdAttribute?.State.ToString(), createdAttribute?.CreatedBy);
        _hookService.HookQueue.Enqueue(CacheKey.Attribute);

        return _mapper.Map<AttributeLibCm>(createdAttribute);
    }

    /// <inheritdoc />
    public async Task<AttributeLibCm> Update(string id, AttributeLibAm attributeAm)
    {
        var validation = attributeAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Attribute is not valid.", validation);

        var attributeToUpdate = _attributeRepository.FindBy(x => x.Id == id, false).Include(x => x.AttributeUnits).FirstOrDefault();

        if (attributeToUpdate == null)
        {
            throw new MimirorgNotFoundException("Attribute not found. Update is not possible.");
        }

        if (attributeToUpdate.State != State.Approved && attributeToUpdate.State != State.Draft)
        {
            throw new MimirorgInvalidOperationException("Update can only be performed on attribute drafts or approved attributes.");
        }

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
                if (attributeUnitDm == null) continue;
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
        var updatedBy = !string.IsNullOrWhiteSpace(_contextAccessor.GetName()) ? _contextAccessor.GetName() : CreatedBy.Unknown;
        await _logService.CreateLog(attributeToUpdate, LogType.Update, attributeToUpdate.State.ToString(), updatedBy);

        _attributeRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Attribute);

        return Get(attributeToUpdate.Id);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail = true)
    {
        var dm = _attributeRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State change on approved attribute with id {id} is not allowed.");

        if (state == State.Approve)
        {
            foreach (var attributeUnit in dm.AttributeUnits)
            {
                var unit = _unitService.Get(attributeUnit.UnitId);

                if (unit.State == State.Approved) continue;
                if (unit.State == State.Deleted) throw new MimirorgInvalidOperationException("Cannot request approval for attribute that uses deleted units.");

                await _unitService.ChangeState(unit.Id, State.Approve);
            }
        }
        else if (state == State.Approved && dm.AttributeUnits.Select(attributeUnit => _unitService.Get(attributeUnit.UnitId)).Any(unit => unit.State != State.Approved))
        {
            throw new MimirorgInvalidOperationException("Cannot approve attribute that uses unapproved units.");
        }

        await _attributeRepository.ChangeState(state == State.Rejected ? State.Draft : state, new List<string> { dm.Id });
        await _logService.CreateLog(dm, LogType.State, state.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        if (state == State.Rejected)
            await _logService.CreateLog(dm, LogType.State, State.Draft.ToString(), dm.CreatedBy);

        _hookService.HookQueue.Enqueue(CacheKey.Attribute);

        if (sendStateEmail)
            await _emailService.SendObjectStateEmail(id, state, dm.Name, ObjectTypeName.Attribute);

        return new ApprovalDataCm
        {
            Id = id,
            State = state == State.Rejected ? State.Draft : state

        };
    }

    /// <inheritdoc />
    public IEnumerable<AttributePredefinedLibCm> GetPredefined()
    {
        var attributes = _attributePredefinedRepository.GetPredefined().Where(x => x.State != State.Deleted).ToList()
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
    }
}