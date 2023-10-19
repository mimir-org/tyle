using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Application.Attributes;
using Tyle.Application.Terminals;
using Tyle.Converters;

namespace Tyle.Api.Common;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Export services")]
public class ExportController : ControllerBase
{
    private readonly IAttributeRepository _attributeRepository;
    private readonly ITerminalRepository _terminalRepository;

    public ExportController(IAttributeRepository attributeRepository, ITerminalRepository terminalRepository)
    {
        _attributeRepository = attributeRepository;
        _terminalRepository = terminalRepository;
    }

    [HttpGet("attribute/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAttribute([FromRoute] Guid id)
    {
        try
        {
            var attribute = await _attributeRepository.Get(id);

            if (attribute == null)
            {
                return Ok("");
            }

            return Ok(attribute.ToJsonLd());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("terminal/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetTerminal([FromRoute] Guid id)
    {
        try
        {
            var terminal = await _terminalRepository.Get(id);

            if (terminal == null)
            {
                return Ok("");
            }

            return Ok(terminal.ToJsonLd());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}