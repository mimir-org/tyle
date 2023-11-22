using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Constants;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Api.Common;
using Tyle.Application.Common;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Common;

namespace Tyle.Api.Terminals;

[Produces(MediaTypeNames.Application.Json)]
[Authorize]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Terminal services")]
public class TerminalsController : ControllerBase
{
    private readonly IMimirorgAuthService _authService;
    private readonly IMapper _mapper;
    private readonly ITerminalRepository _terminalRepository;
    private readonly IApprovalService _approvalService;

    public TerminalsController(IMapper mapper, ITerminalRepository terminalRepository, IApprovalService approvalService, IMimirorgAuthService authService)
    {
        _authService = authService;
        _mapper = mapper;
        _terminalRepository = terminalRepository;
        _approvalService = approvalService;
    }

    /// <summary>
    /// Get all terminals
    /// </summary>
    /// <returns>A collection of terminals</returns>
    [HttpGet]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(ICollection<TerminalView>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] State? state = null)
    {
        try
        {
            var terminals = await _terminalRepository.GetAll(state);
            return Ok(_mapper.Map<IEnumerable<TerminalView>>(terminals));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get terminal by id
    /// </summary>
    /// <param name="id">The id of the terminal to get</param>
    /// <returns>The requested terminal</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(TerminalView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var terminal = await _terminalRepository.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TerminalView>(terminal));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Create a terminal
    /// </summary>
    /// <param name="request">The terminal that should be created</param>
    /// <returns>The created terminal</returns>
    [HttpPost]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(TerminalView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] TerminalTypeRequest request)
    {
        try
        {          
            var createdTerminal = await _terminalRepository.Create(request);
            return Created("dummy", _mapper.Map<TerminalView>(createdTerminal));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update a terminal object
    /// </summary>
    /// <param name="id">The id of the terminal object that should be updated</param>
    /// <param name="request">The new values of the terminal</param>
    /// <returns>The updated terminal</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(TerminalView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TerminalTypeRequest request)
    {
        try
        {           
            var terminal = await _terminalRepository.Update(id, request);

            if (terminal == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TerminalView>(terminal));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Change the state of a terminal.
    /// </summary>
    /// <param name="id">The id of the terminal that will change state.</param>
    /// <param name="request">A request containing the wanted state.</param>
    [HttpPatch("{id}/state")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeState([FromRoute] Guid id, [FromBody] StateChangeRequest request)
    {
        try
        {
            if (!_authService.HasUserPermissionToUpdateToState(User, request.State))
            {
                return StatusCode(403);
            }

            var response = await _approvalService.ChangeTerminalState(id, request.State);

            return response switch
            {
                ApprovalResponse.Accepted => NoContent(),
                ApprovalResponse.TypeNotFound => NotFound(),
                ApprovalResponse.IllegalChange => BadRequest(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete a terminal that is not approved
    /// </summary>
    /// <param name="id">The id of the terminal to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            var terminalFromDb = await _terminalRepository.Get(id);
            if (terminalFromDb == null)
                return NotFound();

            if (!_authService.HasUserPermissionToDelete(User, terminalFromDb))
            {
                return StatusCode(403);
            }

            if (await _terminalRepository.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}