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
using Microsoft.EntityFrameworkCore;
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
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly IHttpContextAccessor _contextAccessor;

    public AttributeService(IMapper mapper, IAttributePredefinedRepository attributePredefinedRepository, IEfAttributeRepository attributeRepository, IEfAttributeUnitRepository attributeUnitRepository, ITimedHookService hookService, ILogService logService, IHttpContextAccessor contextAccessor)
    {
        _mapper = mapper;
        _attributePredefinedRepository = attributePredefinedRepository;
        _attributeRepository = attributeRepository;
        _attributeUnitRepository = attributeUnitRepository;
        _hookService = hookService;
        _logService = logService;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Get all attributes and their units
    /// </summary>
    /// <returns>List of attributes and their units></returns>
    public IEnumerable<AttributeLibCm> Get()
    {
        var dataSet = _attributeRepository.Get().ExcludeDeleted().ToList();

        if (dataSet == null)
            throw new MimirorgNotFoundException("No attributes were found.");

        return !dataSet.Any() ? new List<AttributeLibCm>() : _mapper.Map<List<AttributeLibCm>>(dataSet);
    }

    /// <summary>
    /// Get an attribute and its units
    /// </summary>
    /// <returns>Attribute and its units></returns>
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

        if (attributeToUpdate == null || attributeToUpdate.State == State.Deleted)
        {
            validation = new Validation(new List<string> { nameof(AttributeLibAm.Name) },
                $"Attribute with name {attributeAm.Name} and id {id} does not exist or is flagged as deleted.");
            throw new MimirorgBadRequestException("Attribute does not exist or is flagged as deleted. Update is not possible.", validation);
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
                attributeToUpdate.AttributeUnits.Add(new AttributeUnitLibDm
                {
                    Id = Guid.NewGuid().ToString(),
                    AttributeId = attributeToUpdate.Id,
                    UnitId = attributeUnit.UnitId,
                    IsDefault = attributeUnit.IsDefault
                });
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
    public async Task<ApprovalDataCm> ChangeState(string id, State state)
    {
        var dm = _attributeRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException(
                $"State change on approved attribute with id {id} is not allowed.");

        await _attributeRepository.ChangeState(state, new List<string> { dm.Id });

        await _logService.CreateLog(
            dm,
            LogType.State,
            state.ToString(),
            !string.IsNullOrWhiteSpace(_contextAccessor.GetName()) ? _contextAccessor.GetName() : CreatedBy.Unknown);

        _hookService.HookQueue.Enqueue(CacheKey.Attribute);

        return new ApprovalDataCm
        {
            Id = id,
            State = state

        };
    }

    /// <summary>
    /// Get predefined attributes
    /// </summary>
    /// <returns>List of predefined attributes</returns>
    public IEnumerable<AttributePredefinedLibCm> GetPredefined()
    {
        var attributes = _attributePredefinedRepository.GetPredefined().Where(x => x.State != State.Deleted).ToList()
            .OrderBy(x => x.Aspect).ThenBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).ToList();

        return _mapper.Map<List<AttributePredefinedLibCm>>(attributes);
    }

    /// <summary>
    /// Create predefined attributes
    /// </summary>
    /// <param name="predefined"></param>
    /// <returns>Created predefined attribute</returns>
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