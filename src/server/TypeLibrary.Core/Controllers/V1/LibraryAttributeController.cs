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
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
using Mimirorg.Common.Enums;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;

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
    private readonly IMimirorgAuthService _authService;
    private readonly ILogService _logService;

    public LibraryAttributeController(ILogger<LibraryAttributeController> logger, IAttributeService attributeService, IMimirorgAuthService authService, ILogService logService)
    {
        _logger = logger;
        _attributeService = attributeService;
        _authService = authService;
        _logService = logService;
    }

    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>A collection of attributes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AttributeLibCm>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _attributeService.Get().ToList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get attribute by id
    /// </summary>
    /// <param name="id">The id of the attribute to get</param>
    /// <returns>The requested attribute</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get([FromRoute] int id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = _attributeService.Get(id);

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
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Create an attribute
    /// </summary>
    /// <param name="attribute">The attribute that should be created</param>
    /// <returns>The created attribute</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "attribute", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] AttributeLibAm attribute)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _attributeService.Create(attribute);
            return Ok(cm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Update an attribute with a new state
    /// </summary>
    /// <param name="id">The id of the attribute to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the attribute and the new state</returns>
    [HttpPatch("{id}/state/{state}")]
    [ProducesResponseType(typeof(ApprovalDataCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> ChangeState([FromRoute] int id, [FromRoute] State state)
    {
        try
        {
            var companyId = await _attributeService.GetCompanyId(id);
            var hasAccess = await _authService.HasAccess(companyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _attributeService.ChangeState(id, state);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Reject a state change request and revert the attribute to its previous state
    /// </summary>
    /// <param name="id">The id of the attribute with the requested state change</param>
    /// <returns>An approval data object containing the id of the attribute and the reverted state</returns>
    [HttpPatch("{id}/state/reject")]
    [ProducesResponseType(typeof(ApprovalDataCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> RejectChangeState([FromRoute] int id)
    {
        try
        {
            var companyId = await _attributeService.GetCompanyId(id);
            var previousState = await _logService.GetPreviousState(id, nameof(AttributeLibDm));
            var hasAccess = await _authService.HasAccess(companyId, previousState);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _attributeService.ChangeState(id, previousState);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get all predefined attributes
    /// </summary>
    /// <returns>A collection of predefined attributes</returns>
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