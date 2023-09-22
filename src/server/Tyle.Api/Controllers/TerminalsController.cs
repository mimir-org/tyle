using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Terminals;

namespace Tyle.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
[ApiController]
public class TerminalsController : ControllerBase
{
    private readonly ITerminalService _terminalService;

    public TerminalsController(ITerminalService terminalService)
    {
        _terminalService = terminalService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TerminalType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var terminals = await _terminalService.GetAll();
            return Ok(terminals);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TerminalType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var terminal = await _terminalService.Get(id);
            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(TerminalType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] TerminalTypeRequest request)
    {
        try
        {
            return Ok(await _terminalService.Create(request));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TerminalType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TerminalTypeRequest request)
    {
        try
        {
            return Ok(await _terminalService.Update(id, request));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _terminalService.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }
}
