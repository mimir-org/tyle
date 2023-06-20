using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Quantity datum services")]
public class LibraryQuantityDatumController : ControllerBase
{
    private readonly ILogger<LibraryQuantityDatumController> _logger;
    private readonly IQuantityDatumService _quantityDatumService;
    private readonly IMimirorgAuthService _authService;

    public LibraryQuantityDatumController(ILogger<LibraryQuantityDatumController> logger, IQuantityDatumService quantityDatumService, IMimirorgAuthService authService)
    {
        _logger = logger;
        _quantityDatumService = quantityDatumService;
        _authService = authService;
    }

    /// <summary>
    /// Get all quantity datums
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<QuantityDatumLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get quantity datum by id
    /// </summary>
    /// <param name="id">The id of the quantity datum to get</param>
    /// <returns>The requested quantity datum</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(QuantityDatumLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get([FromRoute] string id)
    {
        try
        {
            var data = _quantityDatumService.Get(id);
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
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get all quantity datums of a given type
    /// </summary>
    /// <param name="type">The type of the quantity datum you want to receive</param>
    /// <returns>A collection of quantity datums</returns>
    [HttpGet("type/{type}")]
    [ProducesResponseType(typeof(ICollection<QuantityDatumLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get(QuantityDatumType type)
    {
        try
        {
            var data = type switch
            {
                QuantityDatumType.QuantityDatumRangeSpecifying => _quantityDatumService.GetQuantityDatumRangeSpecifying(),
                QuantityDatumType.QuantityDatumRegularitySpecified => _quantityDatumService.GetQuantityDatumRegularitySpecified(),
                QuantityDatumType.QuantityDatumSpecifiedProvenance => _quantityDatumService.GetQuantityDatumSpecifiedProvenance(),
                QuantityDatumType.QuantityDatumSpecifiedScope => _quantityDatumService.GetQuantityDatumSpecifiedScope(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Enum type ({nameof(QuantityDatumType)}): {type} out of range.")
            };

            data ??= new List<QuantityDatumLibCm>();

            return Ok(data.ToList());
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        catch (MimirorgBadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Update a quantity datum
    /// </summary>
    /// <param name="id">The id of the quantity datum that should be updated</param>
    /// <param name="quantityDatum">The new values of the quantity datum</param>
    /// <returns>The updated quantity datum</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(QuantityDatumLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "quantityDatum", "CompanyId")]
    public async Task<IActionResult> Update(string id, [FromBody] QuantityDatumLibAm quantityDatum)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _quantityDatumService.Update(id, quantityDatum);
            return Ok(data);
        }
        catch (MimirorgNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MimirorgBadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (MimirorgInvalidOperationException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Delete a quantity datum that is not approved
    /// </summary>
    /// <param name="id">The id of the quantity datum to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        try
        {
            var quantityDatum = _quantityDatumService.Get(id);
            var hasAccess = await _authService.CanDelete(quantityDatum.State, quantityDatum.CreatedBy, CompanyConstants.AnyCompanyId);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            await _quantityDatumService.Delete(id);
            return NoContent();
        }
        catch (MimirorgNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MimirorgInvalidOperationException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> ChangeState([FromRoute] string id, [FromRoute] State state)
    {
        try
        {
            var hasAccess = await _authService.HasAccess(CompanyConstants.AnyCompanyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _quantityDatumService.ChangeState(id, state, true);
            return Ok(data);
        }
        catch (MimirorgNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MimirorgInvalidOperationException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}