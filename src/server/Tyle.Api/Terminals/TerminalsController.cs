using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Api.Common;
using Tyle.Application.Common;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;

namespace Tyle.Api.Terminals;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Terminal services")]
public class TerminalsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITerminalRepository _terminalRepository;
    private readonly IApprovalService _approvalService;

    public TerminalsController(IMapper mapper, ITerminalRepository terminalRepository, IApprovalService approvalService)
    {
        _mapper = mapper;
        _terminalRepository = terminalRepository;
        _approvalService = approvalService;
    }

    /// <summary>
    /// Get all terminals
    /// </summary>
    /// <returns>A collection of terminals</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<TerminalView>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var terminals = await _terminalRepository.GetAll();
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
    [ProducesResponseType(typeof(TerminalView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
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
    [ProducesResponseType(typeof(TerminalView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
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
    [ProducesResponseType(typeof(TerminalView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
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
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> ChangeState([FromRoute] Guid id, [FromBody] StateChangeRequest request)
    {
        try
        {
            // TODO: Handle authorization

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
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
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