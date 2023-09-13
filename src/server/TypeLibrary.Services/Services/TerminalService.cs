using AutoMapper;
using Lucene.Net.Util;
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
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Repositories.Ef;
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
    private readonly IEfClassifierRepository _classifierRepository;
    private readonly IEfPurposeRepository _purposeRepository;
    private readonly IEfMediumRepository _mediumRepository;

    public TerminalService(IEfTerminalRepository terminalRepository, IMapper mapper, ITimedHookService hookService, ILogService logService, ILogger<TerminalService> logger, IAttributeService attributeService, IAttributeRepository attributeRepository, IEfTerminalAttributeRepository terminalAttributeRepository, IHttpContextAccessor contextAccessor, IEmailService emailService, IEfClassifierRepository classifierRepository, IEfPurposeRepository purposeRepository, IEfMediumRepository mediumRepository)
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
        _classifierRepository = classifierRepository;
        _purposeRepository = purposeRepository;
        _mediumRepository = mediumRepository;
    }

    /// <inheritdoc />
    public IEnumerable<TerminalTypeView> Get()
    {
        var dataSet = _terminalRepository.Get()?.ToList();

        if (dataSet == null || !dataSet.Any())
            return new List<TerminalTypeView>();

        return _mapper.Map<List<TerminalTypeView>>(dataSet);
    }

    /// <inheritdoc />
    public TerminalTypeView Get(Guid id)
    {
        var dm = _terminalRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

        return _mapper.Map<TerminalTypeView>(dm);
    }

    /// <inheritdoc />
    public async Task<TerminalTypeView> Create(TerminalTypeRequest terminal)
    {
        if (terminal == null)
            throw new ArgumentNullException(nameof(terminal));

        var validation = terminal.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Terminal is not valid.", validation);

        var dm = _mapper.Map<TerminalType>(terminal);
        dm.LastUpdateOn = dm.CreatedOn;

        foreach (var classifierReferenceId in terminal.ClassifierReferenceIds)
        {
            // TODO: Add null checking

            dm.Classifiers.Add(await _classifierRepository.GetAsync(classifierReferenceId));
        }

        if (terminal.PurposeReferenceId != null)
        {
            dm.Purpose = await _purposeRepository.GetAsync((int)terminal.PurposeReferenceId);
        }

        if (terminal.MediumReferenceId != null)
        {
            dm.Medium = await _mediumRepository.GetAsync((int)terminal.MediumReferenceId);
        }

        foreach (var terminalAttribute in terminal.TerminalAttributes)
        {
            dm.TerminalAttributes.Add(new TerminalAttributeTypeReference()
            {
                MinCount = terminalAttribute.MinCount,
                MaxCount = terminalAttribute.MaxCount,
                TerminalId = dm.Id,
                AttributeId = terminalAttribute.AttributeId
            });
        }

        //dm.State = State.Draft;
        /*dm.TerminalAttributes = new List<TerminalAttributeTypeReference>();

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
                    dm.TerminalAttributes.Add(new TerminalAttributeTypeReference { TerminalId = dm.Id, AttributeId = attribute.Id });
                }
            }
        }*/

        var createdTerminal = await _terminalRepository.Create(dm);
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);
        _terminalRepository.ClearAllChangeTrackers();
        //await _logService.CreateLog(createdTerminal, LogType.Create, createdTerminal.State.ToString(), createdTerminal.CreatedBy);

        return Get(createdTerminal.Id);
    }

    /*/// <inheritdoc />
    public async Task<TerminalTypeView> Update(string id, TerminalTypeRequest terminalAm)
    {
        var validation = terminalAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Terminal is not valid.", validation);

        var terminalToUpdate = _terminalRepository.FindBy(x => x.Id == id, false).Include(x => x.TerminalAttributes).FirstOrDefault();

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
            terminalToUpdate.TerminalAttributes ??= new List<TerminalAttributeTypeReference>();

            var currentTerminalAttributes = terminalToUpdate.TerminalAttributes.ToHashSet();
            var newTerminalAttributes = new HashSet<TerminalAttributeTypeReference>();

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
                        newTerminalAttributes.Add(new TerminalAttributeTypeReference { TerminalId = id, AttributeId = attribute.Id });
                    }
                }
            }

            foreach (var terminalAttribute in currentTerminalAttributes.ExceptBy(newTerminalAttributes.Select(x => x.AttributeId), y => y.AttributeId))
            {
                var terminalAttributeToDelete = _terminalAttributeRepository.FindBy(x => x.TerminalId == terminalToUpdate.Id && x.AttributeId == terminalAttribute.AttributeId).FirstOrDefault();

                if (terminalAttributeToDelete == null)
                    continue;

                await _terminalAttributeRepository.Delete(terminalAttributeToDelete.Id);
            }

            foreach (var terminalAttribute in newTerminalAttributes.ExceptBy(currentTerminalAttributes.Select(x => x.AttributeId), y => y.AttributeId))
            {
                terminalToUpdate.TerminalAttributes.Add(new TerminalAttributeTypeReference
                {
                    TerminalId = terminalToUpdate.Id,
                    AttributeId = terminalAttribute.AttributeId
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
    }*/

    /// <inheritdoc />
    public async Task Delete(Guid id)
    {
        var dm = _terminalRepository.Get(id) ?? throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

        /*if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"Can't delete approved terminal with id {id}.");*/

        await _terminalRepository.Delete(id);
        await _terminalRepository.SaveAsync();
    }

    /*/// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
    {
        var dm = _terminalRepository.Get(id) ?? throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State '{state}' is not allowed for object {dm.Name} with id {dm.Id} since current state is {dm.State}");

        if (state == State.Review)
        {
            foreach (var terminalAttribute in dm.TerminalAttributes)
            {
                if (terminalAttribute.Attribute.State == State.Approved)
                    continue;

                await _attributeService.ChangeState(terminalAttribute.AttributeId, State.Review, true);
            }
        }
        else if (state == State.Approved && dm.TerminalAttributes.Any(terminalAttribute => terminalAttribute.Attribute.State != State.Approved))
        {
            throw new MimirorgInvalidOperationException("Cannot approve terminal that uses unapproved attributes.");
        }

        await _terminalRepository.ChangeState(state, dm.Id);
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);
        await _logService.CreateLog(dm, LogType.State, state.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        if (sendStateEmail)
            await _emailService.SendObjectStateEmail(dm.Id, state, dm.Name, ObjectTypeName.Terminal);

        return new ApprovalDataCm { Id = dm.Id, State = state };
    }*/
}