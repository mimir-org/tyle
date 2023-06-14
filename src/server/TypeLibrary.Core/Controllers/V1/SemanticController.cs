using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

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
    private readonly IAspectObjectService _aspectObjectService;
    private readonly ITerminalService _terminalService;
    private readonly IUnitService _unitService;
    private readonly IQuantityDatumService _quantityDatumService;
    private readonly IRdsService _rdsService;
    private readonly ISymbolService _symbolService;

    public SemanticController(ILogger<SemanticController> logger, IAttributeService attributeService, IAspectObjectService aspectObjectService, ITerminalService terminalService, IUnitService unitService, IQuantityDatumService quantityDatumService, IRdsService rdsService, ISymbolService symbolService)
    {
        _logger = logger;
        _attributeService = attributeService;
        _aspectObjectService = aspectObjectService;
        _terminalService = terminalService;
        _unitService = unitService;
        _quantityDatumService = quantityDatumService;
        _rdsService = rdsService;
        _symbolService = symbolService;
    }

    /// <summary>
    /// Get aspect object ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // ReSharper disable once StringLiteralTypo
    [HttpGet("aspectobject/{id}")]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetAspectObject(string id)
    {
        try
        {
            var data = _aspectObjectService.Get(id);
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
    [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetAttribute(string id)
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
    [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetTerminal(string id)
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
    /// Get unit ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("unit/{id}")]
    [ProducesResponseType(typeof(UnitLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetUnit(string id)
    {
        try
        {
            var data = _unitService.Get(id);
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
    /// Get quantity datum ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("quantitydatum/{id}")]
    [ProducesResponseType(typeof(QuantityDatumLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetQuantityDatum(string id)
    {
        try
        {
            var data = _quantityDatumService.Get(id);
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
    /// Get RDS ontology
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("rds/{id}")]
    [ProducesResponseType(typeof(RdsLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetRds(string id)
    {
        try
        {
            var data = _rdsService.Get(id);
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