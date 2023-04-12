using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

// ReSharper disable StringLiteralTypo

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Terminal services")]
public class LibraryTerminalController : ControllerBase
{
    private readonly ILogger<LibraryTerminalController> _logger;
    private readonly ITerminalService _terminalService;
    private readonly IMimirorgAuthService _authService;
    private readonly ILogService _logService;

    public LibraryTerminalController(ILogger<LibraryTerminalController> logger, ITerminalService terminalService, IMimirorgAuthService authService, ILogService logService)
    {
        _logger = logger;
        _terminalService = terminalService;
        _authService = authService;
        _logService = logService;
    }

    /// <summary>
    /// Get all terminals
    /// </summary>
    /// <returns>A collection of terminals</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<TerminalLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetLatestVersions()
    {
        try
        {
            var data = _terminalService.GetLatestVersions().ToList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get terminal by id
    /// </summary>
    /// <param name="id">The id of the terminal to get</param>
    /// <returns>The requested terminal</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get(string id)
    {
        try
        {
            var data = _terminalService.Get(id);
            if (data == null)
                return NotFound(id);

            return Ok(data);
        }
        catch (MimirorgNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Create a terminal
    /// </summary>
    /// <param name="terminal">The terminal that should be created</param>
    /// <returns>The created terminal</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "terminal", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] TerminalLibAm terminal)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _terminalService.Create(terminal);
            return Ok(cm);
        }
        catch (MimirorgBadRequestException e)
        {
            foreach (var error in e.Errors().ToList())
            {
                ModelState.Remove(error.Key);
                ModelState.TryAddModelError(error.Key, error.Error);
            }

            return BadRequest(ModelState);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Update a terminal
    /// </summary>
    /// <param name="id">The id of the terminal that should be updated</param>
    /// <param name="terminal">The new values of the terminal</param>
    /// <returns>The updated terminal</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "terminal", "CompanyId")]
    public async Task<IActionResult> Update(string id, [FromBody] TerminalLibAm terminal)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = await _terminalService.GetCompanyId(id);

            if (companyId != terminal.CompanyId)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _terminalService.Update(id, terminal);
            return Ok(data);
        }
        catch (MimirorgBadRequestException e)
        {
            foreach (var error in e.Errors().ToList())
            {
                ModelState.Remove(error.Key);
                ModelState.TryAddModelError(error.Key, error.Error);
            }

            return BadRequest(ModelState);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Update a terminal with a new state
    /// </summary>
    /// <param name="id">The id of the terminal to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the terminal and the new state</returns>
    [HttpPatch("{id}/state/{state}")]
    [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> ChangeState([FromRoute] string id, [FromRoute] State state)
    {
        try
        {
            var companyId = await _terminalService.GetCompanyId(id);
            var hasAccess = await _authService.HasAccess(companyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _terminalService.ChangeState(id, state);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Reject a state change request and revert the terminal to its previous state
    /// </summary>
    /// <param name="id">The id of the terminal with the requested state change</param>
    /// <returns>An approval data object containing the id of the terminal and the reverted state</returns>
    [HttpPatch("{id}/state/reject")]
    [ProducesResponseType(typeof(ApprovalDataCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> RejectChangeState([FromRoute] string id)
    {
        try
        {
            var companyId = await _terminalService.GetCompanyId(id);
            var previousState = await _logService.GetPreviousState(id, nameof(TerminalLibDm));
            var hasAccess = await _authService.HasAccess(companyId, previousState);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _terminalService.ChangeState(id, previousState);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}