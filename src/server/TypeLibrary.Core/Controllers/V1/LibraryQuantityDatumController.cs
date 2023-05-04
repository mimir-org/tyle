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
[ApiVersion("1.0")]
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
    /// Get all quantity datums of a given type
    /// </summary>
    /// <param name="type">The type of the quantity datum you want to receive</param>
    /// <returns>A collection of quantity datums</returns>
    [HttpGet("{type}")]
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
            return Forbid(e.Message);
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
                return StatusCode(StatusCodes.Status401Unauthorized);

            var data = await _quantityDatumService.ChangeState(id, state);
            return Ok(data);
        }
        catch (MimirorgNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MimirorgInvalidOperationException e)
        {
            return Forbid(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Reject a state change request by setting the state back to 'Draft'
    /// </summary>
    /// <param name="id">The id of the quantity datum with the requested state change</param>
    /// <returns>An approval data object containing the id of the quantity datum and the reverted state</returns>
    [HttpPatch("{id}/state/reject")]
    [ProducesResponseType(typeof(ApprovalDataCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> RejectChangeState([FromRoute] string id)
    {
        try
        {
            var cm = _quantityDatumService.Get(id);

            if (cm == null)
                return StatusCode(StatusCodes.Status404NotFound);

            if (cm.State is State.Draft or State.Deleted or State.Approved)
                return Forbid($"Can't reject a state change for an object with state {cm.State}");

            var hasAccess = await _authService.HasAccess(CompanyConstants.AnyCompanyId, cm.State == State.Approve ? State.Approved : State.Delete);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var data = await _quantityDatumService.ChangeState(id, State.Draft);
            return Ok(data);
        }
        catch (MimirorgNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MimirorgInvalidOperationException e)
        {
            return Forbid(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}