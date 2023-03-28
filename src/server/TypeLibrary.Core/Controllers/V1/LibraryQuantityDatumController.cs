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
using TypeLibrary.Services.Services;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Quantity datum services")]
public class LibraryQuantityDatumController : ControllerBase
{
    private readonly ILogger<LibraryQuantityDatumController> _logger;
    private readonly IQuantityDatumService _quantityDatumService;
    private readonly IMimirorgAuthService _authService;
    private readonly ILogService _logService;

    public LibraryQuantityDatumController(ILogger<LibraryQuantityDatumController> logger, IQuantityDatumService quantityDatumService, IMimirorgAuthService authService, ILogService logService)
    {
        _logger = logger;
        _quantityDatumService = quantityDatumService;
        _authService = authService;
        _logService = logService;
    }

    /// <summary>
    /// Get all quantity datums
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<QuantityDatumLibCm>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _quantityDatumService.Get().ToList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get all quantity datums of a given type
    /// </summary>
    /// <param name="type">The type of the quantity datum you want to receive</param>
    /// <returns>A collection of quantity datums</returns>
    [HttpGet("datum/{type}")]
    [ProducesResponseType(typeof(ICollection<QuantityDatumLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Get(QuantityDatumType type)
    {
        try
        {
            var data = type switch
            {
                QuantityDatumType.QuantityDatumRangeSpecifying => await _quantityDatumService.GetQuantityDatumRangeSpecifying(),
                QuantityDatumType.QuantityDatumRegularitySpecified => await _quantityDatumService.GetQuantityDatumRegularitySpecified(),
                QuantityDatumType.QuantityDatumSpecifiedProvenance => await _quantityDatumService.GetQuantityDatumSpecifiedProvenance(),
                QuantityDatumType.QuantityDatumSpecifiedScope => await _quantityDatumService.GetQuantityDatumSpecifiedScope(),
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

    /// <summary>
    /// Create a quantity datum
    /// </summary>
    /// <param name="quantityDatum">The quantity datum that should be created</param>
    /// <returns>The created quantity datum</returns>
    [HttpPost]
    [ProducesResponseType(typeof(QuantityDatumLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "quantityDatum", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] QuantityDatumLibAm quantityDatum)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _quantityDatumService.Create(quantityDatum);
            return Ok(cm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Update a quantity datum with a new state
    /// </summary>
    /// <param name="id">The id of the quantity datum to update</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the quantity datum and the new state</returns>
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
            var companyId = await _quantityDatumService.GetCompanyId(id);
            var hasAccess = await _authService.HasAccess(companyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _quantityDatumService.ChangeState(id, state);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Reject a state change request and revert the quantity datum to its previous state
    /// </summary>
    /// <param name="id">The id of the quantity datum with the requested state change</param>
    /// <returns>An approval data object containing the id of the quantity datum and the reverted state</returns>
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
            var companyId = await _quantityDatumService.GetCompanyId(id);
            var previousState = await _logService.GetPreviousState(id, nameof(QuantityDatumLibAm));
            var hasAccess = await _authService.HasAccess(companyId, previousState);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _quantityDatumService.ChangeState(id, previousState);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}