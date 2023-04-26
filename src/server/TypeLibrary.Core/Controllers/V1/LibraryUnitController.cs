using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using TypeLibrary.Services.Constants;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Unit services")]
public class LibraryUnitController : ControllerBase
{
    private readonly ILogger<LibraryUnitController> _logger;
    private readonly IUnitService _unitService;
    private readonly IMimirorgAuthService _authService;
    private readonly ILogService _logService;

    public LibraryUnitController(ILogger<LibraryUnitController> logger, IUnitService unitService, IMimirorgAuthService authService, ILogService logService)
    {
        _logger = logger;
        _unitService = unitService;
        _authService = authService;
        _logService = logService;
    }

    /// <summary>
    /// Get all units
    /// </summary>
    /// <returns>A collection of units</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<UnitLibCm>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _unitService.Get().ToList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get unit by id
    /// </summary>
    /// <param name="id">The id of the unit to get</param>
    /// <returns>The requested unit</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UnitLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get([FromRoute] string id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = _unitService.Get(id);

            if (data == null)
                return NotFound(id);

            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Create a unit
    /// </summary>
    /// <param name="unit">The unit that should be created</param>
    /// <returns>The created unit</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UnitLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "unit", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] UnitLibAm unit)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _unitService.Create(unit);
            return Ok(cm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Update a unit
    /// </summary>
    /// <param name="id">The id of the unit that should be updated</param>
    /// <param name="unit">The new values of the unit</param>
    /// <returns>The updated unit</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UnitLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "unit", "CompanyId")]
    public async Task<IActionResult> Update(string id, [FromBody] UnitLibAm unit)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _unitService.Update(id, unit);
            return Ok(data);
        }
        catch (MimirorgBadRequestException e)
        {
            foreach (var error in e.Errors().ToList())
            {
                ModelState.Remove(error.Key);
                ModelState.TryAddModelError(error.Key, error.Error);
            }

            return BadRequest(ModelState);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Update a unit with a new state
    /// </summary>
    /// <param name="id">The id of the unit to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the unit and the new state</returns>
    [HttpPatch("{id}/state/{state}")]
    [ProducesResponseType(typeof(ApprovalDataCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> ChangeState([FromRoute] string id, [FromRoute] State state)
    {
        try
        {
            var hasAccess = await _authService.HasAccess(CompanyConstants.AnyCompanyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _unitService.ChangeState(id, state);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Reject a state change request and revert the unit to its previous state
    /// </summary>
    /// <param name="id">The id of the unit with the requested state change</param>
    /// <returns>An approval data object containing the id of the unit and the reverted state</returns>
    [HttpPatch("{id}/state/reject")]
    [ProducesResponseType(typeof(ApprovalDataCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> RejectChangeState([FromRoute] string id)
    {
        try
        {
            var previousState = await _logService.GetPreviousState(id, nameof(UnitLibDm));
            var hasAccess = await _authService.HasAccess(CompanyConstants.AnyCompanyId, previousState);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _unitService.ChangeState(id, previousState);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}