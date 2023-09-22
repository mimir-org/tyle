using Tyle.Application.Common;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Application.Terminals;

public class TerminalService : ITerminalService
{
    private readonly IReferenceService _referenceService;
    private readonly ITerminalRepository _terminalRepository;

    public TerminalService(IReferenceService referenceService, ITerminalRepository terminalRepository)
    {
        _referenceService = referenceService;
        _terminalRepository = terminalRepository;
    }

    public async Task<IEnumerable<TerminalType>> GetAll()
    {
        return await _terminalRepository.GetAll();
    }

    public async Task<TerminalType?> Get(Guid id)
    {
        return await _terminalRepository.Get(id);
    }

    public async Task<TerminalType> Create(TerminalTypeRequest request)
    {
        var terminal = new TerminalType(request.Name, request.Description, new User("userid"));

        await UpdateTerminalTypeFields(terminal, request);

        return await _terminalRepository.Create(terminal);
    }

    public async Task<TerminalType> Update(Guid id, TerminalTypeRequest request)
    {
        var terminal = await _terminalRepository.Get(id) ?? throw new KeyNotFoundException($"No terminal type with id {id} found.");

        terminal.Name = request.Name;
        terminal.Description = request.Description;

        // TODO: Implement user logic

        terminal.LastUpdateOn = DateTimeOffset.Now;

        await UpdateTerminalTypeFields(terminal, request);

        return await _terminalRepository.Update(terminal);
    }

    public async Task Delete(Guid id)
    {
        await _terminalRepository.Delete(id);
    }

    private async Task UpdateTerminalTypeFields(TerminalType terminal, TerminalTypeRequest request)
    {
        terminal.Classifiers.Clear();

        foreach (var classifierReferenceId in request.ClassifierReferenceIds)
        {
            var classifier = await _referenceService.GetClassifier(classifierReferenceId) ?? throw new ArgumentException($"No classifier with id {classifierReferenceId} found.", nameof(request));
            terminal.Classifiers.Add(classifier);
        }

        if (request.PurposeReferenceId == null)
        {
            terminal.Purpose = null;
        }
        else
        {
            var purpose = await _referenceService.GetPurpose((int) request.PurposeReferenceId) ?? throw new ArgumentException($"No purpose with id {request.PurposeReferenceId} found.", nameof(request));
            terminal.Purpose = purpose;
        }

        terminal.Notation = request.Notation;
        terminal.Symbol = request.Symbol;
        terminal.Aspect = request.Aspect;

        if (request.MediumReferenceId == null)
        {
            terminal.Medium = null;
        }
        else
        {
            var medium = await _referenceService.GetMedium((int)request.MediumReferenceId) ?? throw new ArgumentException($"No medium with id {request.MediumReferenceId} found.", nameof(request));
            terminal.Medium = medium;
        }

        terminal.Qualifier = request.Qualifier;

        // TODO: Attribute references
    }
}
