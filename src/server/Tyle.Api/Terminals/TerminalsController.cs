using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Api.Blocks;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Common;

namespace Tyle.Api.Terminals;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Terminal services")]
public class TerminalsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITerminalRepository _terminalRepository;

    public TerminalsController(IMapper mapper, ITerminalRepository terminalRepository)
    {
        _mapper = mapper;
        _terminalRepository = terminalRepository;
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
    [ProducesResponseType(typeof(ApiResponse<TerminalView>), StatusCodes.Status207MultiStatus)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] TerminalTypeRequest request)
    {
        try
        {
            var createdTerminal = await _terminalRepository.Create(request);

            if (createdTerminal.ErrorMessage.Count > 0)
            {
                return StatusCode(207, (("dummy", _mapper.Map<TerminalView>(createdTerminal.TValue), createdTerminal.ErrorMessage)));
            }

            return Created("dummy", _mapper.Map<TerminalView>(createdTerminal.TValue));
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
    [ProducesResponseType(typeof(ApiResponse<TerminalView>), StatusCodes.Status207MultiStatus)]
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

            if (terminal.ErrorMessage.Count > 0)
            {
                return StatusCode(207, (("dummy", _mapper.Map<TerminalView>(terminal.TValue), terminal.ErrorMessage)));
            }

            return Ok(_mapper.Map<TerminalView>(terminal.TValue));
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