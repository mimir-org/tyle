using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Aspect Object Services")]
public class LibraryAspectObjectController : ControllerBase
{
    private readonly ILogger<LibraryAspectObjectController> _logger;
    private readonly IAspectObjectService _aspectObjectService;
    private readonly IMimirorgAuthService _authService;
    private readonly ILogService _logService;

    public LibraryAspectObjectController(ILogger<LibraryAspectObjectController> logger, IAspectObjectService aspectObjectService, IMimirorgAuthService authService, ILogService logService)
    {
        _logger = logger;
        _aspectObjectService = aspectObjectService;
        _authService = authService;
        _logService = logService;
    }

    /// <summary>
    /// Get all aspect objects
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AspectObjectLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetLatestVersions()
    {
        try
        {
            var cm = _aspectObjectService.GetLatestVersions().ToList();
            return Ok(cm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Get aspect object by id
    /// </summary>
    /// <param name="id">The id of the aspect object to get</param>
    /// <returns>The requested aspect object</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
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

            var data = _aspectObjectService.Get(id);

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
    /// Create an aspect object
    /// </summary>
    /// <param name="aspectObject">The aspect object that should be created</param>
    /// <returns>The created aspect object</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "aspectObject", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] AspectObjectLibAm aspectObject)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _aspectObjectService.Create(aspectObject);
            return Ok(cm);
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
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Update an aspect object
    /// </summary>
    /// <param name="id">The id of the aspect object that should be updated</param>
    /// <param name="aspectObjectAm">The new values of the aspect object</param>
    /// <returns>The updated aspect object</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "aspectObjectAm", "CompanyId")]
    public async Task<IActionResult> Update(int id, [FromBody] AspectObjectLibAm aspectObjectAm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = await _aspectObjectService.GetCompanyId(id);

            if (companyId != aspectObjectAm.CompanyId)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _aspectObjectService.Update(id, aspectObjectAm);
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
    /// Update an aspect object with a new state
    /// </summary>
    /// <param name="id">The id of the aspect object to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the aspect object and the new state</returns>
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
            var companyId = await _aspectObjectService.GetCompanyId(id);
            var hasAccess = await _authService.HasAccess(companyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _aspectObjectService.ChangeState(id, state);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Reject a state change request and revert the aspect object to its previous state
    /// </summary>
    /// <param name="id">The id of the aspect object with the requested state change</param>
    /// <returns>An approval data object containing the id of the aspect object and the reverted state</returns>
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
            var companyId = await _aspectObjectService.GetCompanyId(id);
            var previousState = await _logService.GetPreviousState(id, nameof(AspectObjectLibDm));
            var hasAccess = await _authService.HasAccess(companyId, previousState);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _aspectObjectService.ChangeState(id, previousState);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}