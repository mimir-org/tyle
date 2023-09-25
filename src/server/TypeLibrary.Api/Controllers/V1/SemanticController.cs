using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Api.Controllers.V1;

/// <summary>
/// TypeCm file services
/// </summary>
[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/ont")]
[SwaggerTag("Ontology Services")]
public class SemanticController : ControllerBase
{
    private readonly ILogger<SemanticController> _logger;
    private readonly IAttributeService _attributeService;
    private readonly IBlockService _blockService;
    private readonly ITerminalService _terminalService;
    private readonly ISymbolService _symbolService;

    public SemanticController(ILogger<SemanticController> logger, IAttributeService attributeService, IBlockService blockService, ITerminalService terminalService, ISymbolService symbolService)
    {
        _logger = logger;
        _attributeService = attributeService;
        _blockService = blockService;
        _terminalService = terminalService;
        _symbolService = symbolService;
    }

    /// <summary>
    /// Get block ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // ReSharper disable once StringLiteralTypo
    [HttpGet("block/{id}")]
    [ProducesResponseType(typeof(BlockTypeView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetBlock(Guid id)
    {
        try
        {
            var data = _blockService.Get(id);
            if (data == null)
                return NoContent();

            return Ok(data);
        }
        catch (MimirorgBadRequestException e)
        {
            _logger.LogWarning(e, $"Warning error: {e.Message}");

            foreach (var error in e.Errors().ToList())
            {
                ModelState.Remove(error.Key);
                ModelState.TryAddModelError(error.Key, error.Error);
            }

            return BadRequest(ModelState);
        }
        catch (MimirorgNotFoundException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get attribute ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("attribute/{id}")]
    [ProducesResponseType(typeof(AttributeTypeView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetAttribute(Guid id)
    {
        try
        {
            var data = _attributeService.Get(id);

            if (data == null)
                return NoContent();

            return Ok(data);
        }
        catch (MimirorgBadRequestException e)
        {
            _logger.LogWarning(e, $"Warning error: {e.Message}");

            foreach (var error in e.Errors().ToList())
            {
                ModelState.Remove(error.Key);
                ModelState.TryAddModelError(error.Key, error.Error);
            }

            return BadRequest(ModelState);
        }
        catch (MimirorgNotFoundException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get terminal ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("terminal/{id}")]
    [ProducesResponseType(typeof(TerminalTypeView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetTerminal(Guid id)
    {
        try
        {
            var data = _terminalService.Get(id);
            if (data == null)
                return NoContent();

            return Ok(data);
        }
        catch (MimirorgBadRequestException e)
        {
            _logger.LogWarning(e, $"Warning error: {e.Message}");

            foreach (var error in e.Errors().ToList())
            {
                ModelState.Remove(error.Key);
                ModelState.TryAddModelError(error.Key, error.Error);
            }

            return BadRequest(ModelState);
        }
        catch (MimirorgNotFoundException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get symbol ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("symbol/{id}")]
    [ProducesResponseType(typeof(SymbolLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetSymbol(string id)
    {
        try
        {
            var data = _symbolService.Get(id);
            if (data == null)
                return NoContent();

            return Ok(data);
        }
        catch (MimirorgBadRequestException e)
        {
            _logger.LogWarning(e, $"Warning error: {e.Message}");

            foreach (var error in e.Errors().ToList())
            {
                ModelState.Remove(error.Key);
                ModelState.TryAddModelError(error.Key, error.Error);
            }

            return BadRequest(ModelState);
        }
        catch (MimirorgNotFoundException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}