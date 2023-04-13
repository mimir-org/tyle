using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class TerminalService : ITerminalService
{
    private readonly ITerminalRepository _terminalRepository;
    private readonly IMapper _mapper;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly IApplicationSettingsRepository _settings;
    private readonly ILogger<TerminalService> _logger;
    private readonly IAttributeRepository _attributeRepository;

    public TerminalService(ITerminalRepository terminalRepository, IMapper mapper, ITimedHookService hookService, ILogService logService, IApplicationSettingsRepository settings, ILogger<TerminalService> logger, IAttributeRepository attributeRepository)
    {
        _terminalRepository = terminalRepository;
        _mapper = mapper;
        _hookService = hookService;
        _logService = logService;
        _settings = settings;
        _logger = logger;
        _attributeRepository = attributeRepository;
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
        var dataSet = _terminalRepository.Get().ToList();

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

        dm.Id = Guid.NewGuid().ToString();
        dm.Iri = $"{_settings.ApplicationSemanticUrl}/terminal/{dm.Id}";
        dm.State = State.Draft;

        dm.Attributes = new List<AttributeLibDm>();
        if (terminal.Attributes != null)
        {
            foreach (var attributeId in terminal.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);
                if (attribute == null)
                    _logger.LogError(
                        $"Could not add attribute with id {attributeId} to terminal with id {dm.Id}, attribute not found.");
                else
                    dm.Attributes.Add(attribute);
            }
        }

        var createdTerminal = await _terminalRepository.Create(dm);
        _terminalRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdTerminal, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);

        return Get(createdTerminal.Id);
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
            throw new MimirorgNotFoundException($"Terminal with id {id} not found, or is not latest version.");

        await _terminalRepository.ChangeState(state, dm.Id);
        await _logService.CreateLog(dm, LogType.State, state.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.Terminal);

        return state == State.Deleted ? null : Get(id);
    }

    /// <summary>
    /// Get terminal existing company id for terminal by id
    /// </summary>
    /// <param name="id">The terminal id</param>
    /// <returns>Company id for terminal</returns>
    public int GetCompanyId(string id)
    {
        return _terminalRepository.HasCompany(id);
    }
}