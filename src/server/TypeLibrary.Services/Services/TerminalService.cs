using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

public class TerminalService : ITerminalService
{
    private readonly IEfTerminalRepository _terminalRepository;
    private readonly IMapper _mapper;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly ILogger<TerminalService> _logger;
    private readonly IAttributeService _attributeService;
    private readonly IAttributeRepository _attributeRepository;
    private readonly IEfTerminalAttributeRepository _terminalAttributeRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public TerminalService(IEfTerminalRepository terminalRepository, IMapper mapper, ITimedHookService hookService, ILogService logService, ILogger<TerminalService> logger, IAttributeService attributeService, IAttributeRepository attributeRepository, IEfTerminalAttributeRepository terminalAttributeRepository, IHttpContextAccessor contextAccessor)
    {
        _terminalRepository = terminalRepository;
        _mapper = mapper;
        _hookService = hookService;
        _logService = logService;
        _logger = logger;
        _attributeService = attributeService;
        _attributeRepository = attributeRepository;
        _terminalAttributeRepository = terminalAttributeRepository;
        _contextAccessor = contextAccessor;
    }

    /// <inheritdoc />
    public TerminalLibCm Get(string id)
    {
        var dm = _terminalRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

        return _mapper.Map<TerminalLibCm>(dm);
    }

    /// <inheritdoc />
    public IEnumerable<TerminalLibCm> Get()
    {
        var dataSet = _terminalRepository.Get().ExcludeDeleted().ToList();

        if (dataSet == null)
            throw new MimirorgNotFoundException("No terminals were found.");

        return !dataSet.Any() ? new List<TerminalLibCm>() : _mapper.Map<List<TerminalLibCm>>(dataSet);
    }

    /// <summary>
    /// Create a new terminal
    /// </summary>
    /// <param name="terminal">The terminal that should be created</param>
    /// <returns></returns>
    /// <exception cref="MimirorgBadRequestException">Throws if terminal is not valid</exception>
    /// <exception cref="MimirorgDuplicateException">Throws if terminal already exist</exception>
    /// <remarks>Remember that creating a new terminal could be creating a new version of existing terminal.
    /// They will have the same first version id, but have different version and id.</remarks>
    public async Task<TerminalLibCm> Create(TerminalLibAm terminal)
    {
        if (terminal == null)
            throw new ArgumentNullException(nameof(terminal));

        var validation = terminal.ValidateObject();
        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Terminal is not valid.", validation);

        var dm = _mapper.Map<TerminalLibDm>(terminal);

        dm.State = State.Draft;

        dm.TerminalAttributes = new List<TerminalAttributeLibDm>();
        if (terminal.Attributes != null)
        {
            foreach (var attributeId in terminal.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);
                if (attribute == null)
                    _logger.LogError(
                        $"Could not add attribute with id {attributeId} to terminal with id {dm.Id}, attribute not found.");
                else
                    dm.TerminalAttributes.Add(new TerminalAttributeLibDm()
                    {
                        TerminalId = dm.Id,
                        AttributeId = attribute.Id
                    });
            }
        }

        var createdTerminal = await _terminalRepository.Create(dm);
        _terminalRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdTerminal, LogType.Create, createdTerminal.State.ToString(), createdTerminal.CreatedBy);
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);

        return Get(createdTerminal.Id);
    }

    /// <inheritdoc />
    public async Task<TerminalLibCm> Update(string id, TerminalLibAm terminalAm)
    {
        var validation = terminalAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Terminal is not valid.", validation);

        var terminalToUpdate = _terminalRepository.FindBy(x => x.Id == id, false).Include(x => x.Attributes).FirstOrDefault();

        if (terminalToUpdate == null || terminalToUpdate.State == State.Deleted)
        {
            validation = new Validation(new List<string> { nameof(TerminalLibAm.Name) },
                $"Terminal with name {terminalAm.Name} and id {id} does not exist or is flagged as deleted.");
            throw new MimirorgBadRequestException("Terminal does not exist or is flagged as deleted. Update is not possible.", validation);
        }

        if (terminalToUpdate.State != State.Approved)
        {
            terminalToUpdate.Name = terminalAm.Name;
            terminalToUpdate.TypeReference = terminalAm.TypeReference;
            terminalToUpdate.Color = terminalAm.Color;
            terminalToUpdate.Description = terminalAm.Description;

            terminalToUpdate.Attributes ??= new List<AttributeLibDm>();
            terminalToUpdate.TerminalAttributes ??= new List<TerminalAttributeLibDm>();

            var currentAttributes = terminalToUpdate.Attributes.ToHashSet();
            var newAttributes = new HashSet<AttributeLibDm>();
            if (terminalAm.Attributes != null)
            {
                foreach (var attributeId in terminalAm.Attributes)
                {
                    var attribute = _attributeRepository.Get(attributeId);
                    if (attribute == null)
                        _logger.LogError(
                            $"Could not add attribute with id {attributeId} to terminal with id {id}, attribute not found.");
                    else
                        newAttributes.Add(attribute);
                }
            }

            foreach (var attribute in currentAttributes.ExceptBy(newAttributes.Select(x => x.Id), y => y.Id))
            {
                var terminalAttribute = _terminalAttributeRepository.FindBy(x => x.TerminalId == terminalToUpdate.Id && x.AttributeId == attribute.Id).FirstOrDefault();
                if (terminalAttribute == null) continue;
                await _terminalAttributeRepository.Delete(terminalAttribute.Id);
            }

            foreach (var attribute in newAttributes.ExceptBy(currentAttributes.Select(x => x.Id), y => y.Id))
            {
                terminalToUpdate.TerminalAttributes.Add(new TerminalAttributeLibDm
                {
                    TerminalId = terminalToUpdate.Id,
                    AttributeId = attribute.Id
                });
            }

            terminalToUpdate.State = State.Draft;
        }
        else
        {
            terminalToUpdate.Color = terminalAm.Color;
            terminalToUpdate.Description = terminalAm.Description;
        }

        await _terminalAttributeRepository.SaveAsync();
        await _terminalRepository.SaveAsync();
        var updatedBy = !string.IsNullOrWhiteSpace(_contextAccessor.GetName()) ? _contextAccessor.GetName() : CreatedBy.Unknown;
        await _logService.CreateLog(terminalToUpdate, LogType.Update, terminalToUpdate.State.ToString(), updatedBy);

        _terminalRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);

        return Get(terminalToUpdate.Id);
    }

    /// <summary>
    /// Change terminal state
    /// </summary>
    /// <param name="id">The terminal id that should change the state</param>
    /// <param name="state">The new terminal state</param>
    /// <returns>Terminal with updated state</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the terminal does not exist on latest version</exception>
    public async Task<TerminalLibCm> ChangeState(string id, State state)
    {
        var dm = _terminalRepository.Get().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgBadRequestException($"State change on approved terminal with id {id} is not allowed.");

        if (state == State.Approve)
        {
            foreach (var attribute in dm.Attributes)
            {
                if (attribute.State == State.Approved) continue;
                if (attribute.State == State.Deleted) throw new MimirorgBadRequestException("Cannot request approval for terminal that uses deleted attributes.");

                await _attributeService.ChangeState(attribute.Id, State.Approve);
            }
        }
        else if (state == State.Approved && dm.Attributes.Any(attribute => attribute.State != State.Approved))
        {
            throw new MimirorgBadRequestException("Cannot approve terminal that uses unapproved attributes.");
        }

        await _terminalRepository.ChangeState(state, dm.Id);

        await _logService.CreateLog(
            dm,
            LogType.State,
            state.ToString(),
            !string.IsNullOrWhiteSpace(_contextAccessor.GetName()) ? _contextAccessor.GetName() : CreatedBy.Unknown);

        _hookService.HookQueue.Enqueue(CacheKey.Terminal);

        return state == State.Deleted ? null : Get(id);
    }
}