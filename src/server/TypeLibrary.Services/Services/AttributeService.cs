using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class AttributeService : IAttributeService
{
    private readonly IMapper _mapper;
    private readonly ApplicationSettings _applicationSettings;
    private readonly IAttributePredefinedRepository _attributePredefinedRepository;
    private readonly IQuantityDatumRepository _datumRepository;
    private readonly IAttributeReferenceRepository _attributeReferenceRepository;
    private readonly IAttributeRepository _attributeRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;

    public AttributeService(IMapper mapper, IOptions<ApplicationSettings> applicationSettings, IAttributePredefinedRepository attributePredefinedRepository, IAttributeReferenceRepository attributeReferenceRepository, IAttributeRepository attributeRepository, IQuantityDatumRepository datumRepository, ITimedHookService hookService, ILogService logService)
    {
        _mapper = mapper;
        _attributePredefinedRepository = attributePredefinedRepository;
        _attributeReferenceRepository = attributeReferenceRepository;
        _attributeRepository = attributeRepository;
        _datumRepository = datumRepository;
        _applicationSettings = applicationSettings?.Value;
        _hookService = hookService;
        _logService = logService;
    }

    /// <summary>
    /// Get all attributes and their units
    /// </summary>
    /// <returns>List of attributes and their units></returns>
    public IEnumerable<AttributeLibCm> Get()
    {
        var dataSet = _attributeRepository.Get().ToList();
        return _mapper.Map<List<AttributeLibCm>>(dataSet);
    }

    /// <summary>
    /// Get an attribute and its units
    /// </summary>
    /// <returns>Attribute and its units></returns>
    public AttributeLibCm Get(int id)
    {
        var dm = _attributeRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

        return _mapper.Map<AttributeLibCm>(dm);
    }

    /// <summary>
    /// Create a new attribute
    /// </summary>
    /// <param name="attributeAm">The attribute that should be created</param>
    /// <returns>The created attribute</returns>
    public async Task<AttributeLibCm> Create(AttributeLibAm attributeAm)
    {
        if (attributeAm == null)
            throw new ArgumentNullException(nameof(attributeAm));

        var dm = _mapper.Map<AttributeLibDm>(attributeAm);

        dm.State = State.Draft;

        var createdAttribute = await _attributeRepository.Create(dm);
        _attributeRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdAttribute, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.Attribute);

        return _mapper.Map<AttributeLibCm>(createdAttribute);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(int id, State state)
    {
        var dm = _attributeRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Attribute with id {id} not found, or is not latest version.");

        await _attributeRepository.ChangeState(state, new List<int> { dm.Id });
        await _logService.CreateLog(dm, LogType.State, state.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.Attribute);

        return new ApprovalDataCm
        {
            Id = id,
            State = state

        };
    }

    /// <inheritdoc />
    public async Task<int> GetCompanyId(int id)
    {
        return await _attributeRepository.HasCompany(id);
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
            attribute.CreatedBy = _applicationSettings.System;
            attribute.State = State.ApproveGlobal;
            await _attributePredefinedRepository.CreatePredefined(attribute);
        }
    }

    /// <summary>
    /// Get all quantity datum range specifying
    /// </summary>
    /// <returns>List of quantity datums</returns>
    public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRangeSpecifying()
    {
        var dataSet = await _datumRepository.GetQuantityDatumRangeSpecifying();
        var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }

    /// <summary>
    /// Get all quantity datum specified scopes
    /// </summary>
    /// <returns>List of quantity datums</returns>
    public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedScope()
    {
        var dataSet = await _datumRepository.GetQuantityDatumSpecifiedScope();
        var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }

    /// <summary>
    /// Get all quantity datum with specified provenances
    /// </summary>
    /// <returns>List of quantity datums</returns>
    public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedProvenance()
    {
        var dataSet = await _datumRepository.GetQuantityDatumSpecifiedProvenance();
        var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }

    /// <summary>
    /// Get all quantity datum regularity specified
    /// </summary>
    /// <returns>List of quantity datums</returns>
    public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRegularitySpecified()
    {
        var dataSet = await _datumRepository.GetQuantityDatumRegularitySpecified();
        var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
        return dataCmList.AsEnumerable();
    }
}