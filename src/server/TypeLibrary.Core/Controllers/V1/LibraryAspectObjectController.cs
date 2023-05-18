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
[SwaggerTag("Aspect Object Services")]
public class LibraryAspectObjectController : ControllerBase
{
    private readonly ILogger<LibraryAspectObjectController> _logger;
    private readonly IAspectObjectService _aspectObjectService;
    private readonly IMimirorgAuthService _authService;

    public LibraryAspectObjectController(ILogger<LibraryAspectObjectController> logger, IAspectObjectService aspectObjectService, IMimirorgAuthService authService)
    {
        _logger = logger;
        _aspectObjectService = aspectObjectService;
        _authService = authService;
    }

    /// <summary>
    /// Get all aspect objects
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AspectObjectLibCm>), StatusCodes.Status200OK)]
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
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get aspect object by id
    /// </summary>
    /// <param name="id">The id of the aspect object to get</param>
    /// <returns>The requested aspect object</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get([FromRoute] string id)
    {
        try
        {
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
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get latest approved version of an aspect object by id
    /// </summary>
    /// <param name="id">The id of the aspect object we want to get the latest approved version of</param>
    /// <returns>The requested aspect object</returns>
    [HttpGet("latestApproved/{id}")]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetLatestApproved([FromRoute] string id)
    {
        try
        {
            var data = _aspectObjectService.GetLatestApproved(id);
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
    /// Create an aspect object
    /// </summary>
    /// <param name="aspectObject">The aspect object that should be created</param>
    /// <returns>The created aspect object</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Update an aspect object
    /// </summary>
    /// <param name="id">The id of the aspect object that should be updated</param>
    /// <param name="aspectObject">The new values of the aspect object</param>
    /// <returns>The updated aspect object</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AspectObjectLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "aspectObject", "CompanyId")]
    public async Task<IActionResult> Update(string id, [FromBody] AspectObjectLibAm aspectObject)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = _aspectObjectService.GetCompanyId(id);

            if (companyId != aspectObject.CompanyId)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _aspectObjectService.Update(id, aspectObject);
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
    /// Update an aspect object with a new state
    /// </summary>
    /// <param name="id">The id of the aspect object to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the aspect object and the new state</returns>
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
            var companyId = _aspectObjectService.GetCompanyId(id);
            var hasAccess = await _authService.HasAccess(companyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var data = await _aspectObjectService.ChangeState(id, state);
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

    /// <summary>
    /// Reject a state change request by setting the state back to 'Draft'
    /// </summary>
    /// <param name="id">The id of the aspect object with the requested state change</param>
    /// <returns>An approval data object containing the id of the aspect object and the reverted state</returns>
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
            var cm = _aspectObjectService.Get(id);

            if (cm == null)
                return StatusCode(StatusCodes.Status404NotFound);

            if (cm.State is State.Draft or State.Deleted or State.Approved)
                return StatusCode(StatusCodes.Status403Forbidden,
                    $"Can't reject a state change for an object with state {cm.State}");

            var hasAccess = await _authService.HasAccess(_aspectObjectService.GetCompanyId(id), cm.State == State.Approve ? State.Approved : State.Delete);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var data = await _aspectObjectService.ChangeState(id, State.Draft);
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