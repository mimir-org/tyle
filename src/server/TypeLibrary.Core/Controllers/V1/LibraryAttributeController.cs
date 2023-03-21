using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.TypeLibrary.Enums;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Attribute services")]
public class LibraryAttributeController : ControllerBase
{
    private readonly ILogger<LibraryAttributeController> _logger;
    private readonly IAttributeService _attributeService;

    public LibraryAttributeController(ILogger<LibraryAttributeController> logger, IAttributeService attributeService)
    {
        _logger = logger;
        _attributeService = attributeService;
    }

    /// <summary>
    /// Get all attributes and their units
    /// </summary>
    /// <returns>A collection of attributes and their units></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AttributeLibCm>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _attributeService.Get();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("predefined")]
    [ProducesResponseType(typeof(ICollection<AttributePredefinedLibCm>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public IActionResult GetPredefined()
    {
        try
        {
            var data = _attributeService.GetPredefined().ToList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get all datums of a given type
    /// </summary>
    /// <param name="type">The type of the quantity datum you want to receive</param>
    /// <returns>A collection of quantity datums</returns>
    [HttpGet("datum/{type}")]
    [ProducesResponseType(typeof(ICollection<QuantityDatumCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDatums(QuantityDatumType type)
    {
        try
        {
            var data = type switch
            {
                QuantityDatumType.QuantityDatumRangeSpecifying => await _attributeService.GetQuantityDatumRangeSpecifying(),
                QuantityDatumType.QuantityDatumRegularitySpecified => await _attributeService.GetQuantityDatumRegularitySpecified(),
                QuantityDatumType.QuantityDatumSpecifiedProvenance => await _attributeService.GetQuantityDatumSpecifiedProvenance(),
                QuantityDatumType.QuantityDatumSpecifiedScope => await _attributeService.GetQuantityDatumSpecifiedScope(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Enum type ({nameof(QuantityDatumType)}): {type} out of range.")
            };

            return Ok(data?.ToList());
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}