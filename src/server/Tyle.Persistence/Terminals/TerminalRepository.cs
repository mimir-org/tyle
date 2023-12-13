using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Abstractions;
using System;
using Tyle.Application.Common;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Converters;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class TerminalRepository : ITerminalRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<TerminalType> _dbSet;
    private readonly IDownstreamApi _downstreamApi;
    private readonly IJsonLdConversionService _jsonLdConversionService;
    private readonly IUserInformationService _userInformationService;

    public TerminalRepository(TyleDbContext context, IDownstreamApi downstreamApi, IJsonLdConversionService jsonLdConversionService, IUserInformationService userInformationService)
    {
        _context = context;
        _dbSet = context.Terminals;
        _downstreamApi = downstreamApi;
        _jsonLdConversionService = jsonLdConversionService;
        _userInformationService = userInformationService;
    }

    public async Task<IEnumerable<TerminalType>> GetAll(State? state = null)
    {
        var query = _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Medium)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery();

        if (state != null)
        {
            query = query.Where(x => x.State == state);
        }

        return await query.ToListAsync();
    }

    public async Task<TerminalType?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Medium)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TerminalType> Create(TerminalTypeRequest request)
    {
        var terminal = new TerminalType
        {
            Name = request.Name,
            Description = request.Description,
            Version = "1.0",
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId(),
            State = State.Draft,
            PurposeId = request.PurposeId,
            Notation = request.Notation,
            Symbol = request.Symbol,
            Aspect = request.Aspect,
            MediumId = request.MediumId,
            Qualifier = request.Qualifier
        };

        terminal.LastUpdateOn = terminal.CreatedOn;

        terminal.Classifiers = request.ClassifierIds.Select(classifierId => new TerminalClassifierJoin { TerminalId = terminal.Id, ClassifierId = classifierId }).ToList();

        terminal.Attributes = request.Attributes.Select(x => new TerminalAttributeTypeReference { TerminalId = terminal.Id, AttributeId = x.AttributeId, MinCount = x.MinCount, MaxCount = x.MaxCount }).ToList();

        _dbSet.Add(terminal);
        await _context.SaveChangesAsync();

        return await Get(terminal.Id);
    }

    public async Task<TerminalType?> Update(Guid id, TerminalTypeRequest request)
    {
        var terminal = await _dbSet.AsTracking()
           .Include(x => x.Classifiers)
           .Include(x => x.Attributes)
           .AsSplitQuery()
           .FirstOrDefaultAsync(x => x.Id == id);

        if (terminal == null)
        {
            return null;
        }

        if (terminal.State != State.Draft)
        {
            throw new InvalidOperationException($"Terminals with state '{terminal.State}' cannot be updated.");
        }

        terminal.Name = request.Name;
        terminal.Description = request.Description;
        if (_userInformationService.GetUserId() != terminal.CreatedBy)
        {
            terminal.ContributedBy.Add(_userInformationService.GetUserId());
        }
        terminal.LastUpdateOn = DateTimeOffset.Now;

        var terminalClassifiersToRemove = terminal.Classifiers.Where(x => !request.ClassifierIds.Contains(x.ClassifierId)).ToList();
        foreach (var terminalClassifier in terminalClassifiersToRemove)
        {
            terminal.Classifiers.Remove(terminalClassifier);
        }

        foreach (var classifierId in request.ClassifierIds)
        {
            if (terminal.Classifiers.Any(x => x.ClassifierId == classifierId)) continue;

            terminal.Classifiers.Add(new TerminalClassifierJoin
            {
                TerminalId = id,
                ClassifierId = classifierId
            });
        }

        terminal.PurposeId = request.PurposeId;
        terminal.Notation = request.Notation;
        terminal.Symbol = request.Symbol;
        terminal.Aspect = request.Aspect;
        terminal.MediumId = request.MediumId;
        terminal.Qualifier = request.Qualifier;

        var terminalAttributesToRemove = terminal.Attributes.Where(x => request.Attributes.All(y => y.AttributeId != x.AttributeId)).ToList();
        foreach (var terminalAttribute in terminalAttributesToRemove)
        {
            terminal.Attributes.Remove(terminalAttribute);
        }

        var terminalAttributeComparer = new TerminalAttributeComparer();

        foreach (var attributeTypeReferenceRequest in request.Attributes)
        {
            var terminalAttribute = new TerminalAttributeTypeReference
            {
                TerminalId = id,
                AttributeId = attributeTypeReferenceRequest.AttributeId,
                MinCount = attributeTypeReferenceRequest.MinCount,
                MaxCount = attributeTypeReferenceRequest.MaxCount
            };

            if (terminal.Attributes.Contains(terminalAttribute, terminalAttributeComparer)) continue;

            var terminalAttributeToUpdate = terminal.Attributes.FirstOrDefault(x => x.AttributeId == attributeTypeReferenceRequest.AttributeId);

            if (terminalAttributeToUpdate != null)
            {
                terminalAttributeToUpdate.MinCount = terminalAttribute.MinCount;
                terminalAttributeToUpdate.MaxCount = terminalAttribute.MaxCount;
            }
            else
            {
                terminal.Attributes.Add(terminalAttribute);
            }
        }

        await _context.SaveChangesAsync();

        return await Get(id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var terminal = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (terminal == null)
        {
            return false;
        }

        if (terminal.State == State.Approved)
        {
            throw new InvalidOperationException("Approved terminals cannot be deleted.");
        }

        _dbSet.Remove(terminal);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeState(Guid id, State state)
    {
        var terminal = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (terminal == null)
        {
            return false;
        }

        if (state == State.Approved)
        {
            var completeTerminal = await Get(id);

            if (completeTerminal == null)
            {
                return false;
            }

            var terminalAsJsonLd = await _jsonLdConversionService.ConvertToJsonLd(completeTerminal);

            var postResponse = await _downstreamApi.CallApiForAppAsync("CommonLib", options =>
            {
                options.HttpMethod = "POST";
                options.RelativePath = "/api/imftype/WriteImfType";
                options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";

                options.CustomizeHttpRequestMessage = message =>
                {
                    message.Content = new StringContent(terminalAsJsonLd.ToString(), System.Text.Encoding.UTF8, "application/json-patch+json");
                };
            });

            if (!postResponse.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Post request to Common Library failed.");
            }
        }

        terminal.State = state;
        await _context.SaveChangesAsync();

        return true;
    }
}