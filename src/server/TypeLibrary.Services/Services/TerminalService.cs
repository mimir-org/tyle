using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    private readonly IEmailService _emailService;

    public TerminalService(IEfTerminalRepository terminalRepository, IMapper mapper, ITimedHookService hookService, ILogService logService, ILogger<TerminalService> logger, IAttributeService attributeService, IAttributeRepository attributeRepository, IEfTerminalAttributeRepository terminalAttributeRepository, IHttpContextAccessor contextAccessor, IEmailService emailService)
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
        _emailService = emailService;
    }

    /// <inheritdoc />
    public IEnumerable<TerminalLibCm> Get()
    {
        var dataSet = _terminalRepository.Get()?.ToList();

        if (dataSet == null || !dataSet.Any())
            return new List<TerminalLibCm>();

        return _mapper.Map<List<TerminalLibCm>>(dataSet);
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
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to terminal with id {dm.Id}, attribute not found.");
                }
                else
                {
                    dm.TerminalAttributes.Add(new TerminalAttributeLibDm { TerminalId = dm.Id, AttributeId = attribute.Id });
                }
            }
        }

        var createdTerminal = await _terminalRepository.Create(dm);
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);
        _terminalRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdTerminal, LogType.Create, createdTerminal.State.ToString(), createdTerminal.CreatedBy);

        return Get(createdTerminal.Id);
    }

    /// <inheritdoc />
    public async Task<TerminalLibCm> Update(string id, TerminalLibAm terminalAm)
    {
        var validation = terminalAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Terminal is not valid.", validation);

        var terminalToUpdate = _terminalRepository.FindBy(x => x.Id == id, false).Include(x => x.Attributes).FirstOrDefault();

        if (terminalToUpdate == null)
            throw new MimirorgNotFoundException("Terminal not found. Update is not possible.");

        if (terminalToUpdate.State != State.Approved && terminalToUpdate.State != State.Draft)
            throw new MimirorgInvalidOperationException("Update can only be performed on terminal drafts or approved terminals.");

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
                    {
                        _logger.LogError($"Could not add attribute with id {attributeId} to terminal with id {id}, attribute not found.");
                    }
                    else
                    {
                        newAttributes.Add(attribute);
                    }
                }
            }

            foreach (var attribute in currentAttributes.ExceptBy(newAttributes.Select(x => x.Id), y => y.Id))
            {
                var terminalAttribute = _terminalAttributeRepository.FindBy(x => x.TerminalId == terminalToUpdate.Id && x.AttributeId == attribute.Id).FirstOrDefault();

                if (terminalAttribute == null)
                    continue;

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
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);
        _terminalRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(terminalToUpdate, LogType.Update, terminalToUpdate.State.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        return Get(terminalToUpdate.Id);
    }

    /// <inheritdoc />
    public async Task Delete(string id)
    {
        var dm = _terminalRepository.Get(id) ?? throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"Can't delete approved terminal with id {id}.");

        await _terminalRepository.Delete(id);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
    {
        var dm = _terminalRepository.Get(id) ?? throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State '{state}' is not allowed for object {dm.Name} with id {dm.Id} since current state is {dm.State}");

        if (state == State.Review)
        {
            foreach (var attribute in dm.Attributes)
            {
                if (attribute.State == State.Approved)
                    continue;

                await _attributeService.ChangeState(attribute.Id, State.Review, true);
            }
        }
        else if (state == State.Approved && dm.Attributes.Any(attribute => attribute.State != State.Approved))
        {
            throw new MimirorgInvalidOperationException("Cannot approve terminal that uses unapproved attributes.");
        }

        await _terminalRepository.ChangeState(state, dm.Id);
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);
        await _logService.CreateLog(dm, LogType.State, state.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        if (sendStateEmail)
            await _emailService.SendObjectStateEmail(dm.Id, state, dm.Name, ObjectTypeName.Terminal);

        return new ApprovalDataCm { Id = dm.Id, State = state };
    }
}