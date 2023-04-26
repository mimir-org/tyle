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
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
using Mimirorg.TypeLibrary.Constants;

namespace TypeLibrary.Core.Controllers.V1;

/// <summary>
/// TypeCm file services
/// </summary>
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("RDS services")]
public class LibraryRdsController : ControllerBase
{
    private readonly ILogger<LibraryRdsController> _logger;
    private readonly IRdsService _rdsService;
    private readonly IMimirorgAuthService _authService;
    private readonly ILogService _logService;

    public LibraryRdsController(ILogger<LibraryRdsController> logger, IRdsService rdsService, IMimirorgAuthService authService, ILogService logService)
    {
        _logger = logger;
        _rdsService = rdsService;
        _authService = authService;
        _logService = logService;
    }

    /// <summary>
    /// Get all RDS objects
    /// </summary>
    /// <returns>A collection of RDS objects</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<RdsLibCm>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _rdsService.Get();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get RDS by id
    /// </summary>
    /// <param name="id">The id of the RDS to get</param>
    /// <returns>The requested RDS</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RdsLibCm), StatusCodes.Status200OK)]
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

            var data = _rdsService.Get(id);

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
    /// Create a RDS object
    /// </summary>
    /// <param name="rds">The RDS that should be created</param>
    /// <returns>The created RDS</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RdsLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "rds", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] RdsLibAm rds)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _rdsService.Create(rds);
            return Ok(cm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Update a RDS object
    /// </summary>
    /// <param name="id">The id of the RDS that should be updated</param>
    /// <param name="rds">The new values of the RDS</param>
    /// <returns>The updated RDS</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RdsLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "rds", "CompanyId")]
    public async Task<IActionResult> Update(string id, [FromBody] RdsLibAm rds)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _rdsService.Update(id, rds);
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
    /// Update a RDS object with a new state
    /// </summary>
    /// <param name="id">The id of the RDS to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the RDS and the new state</returns>
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

            var data = await _rdsService.ChangeState(id, state);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Reject a state change request and revert the RDS to its previous state
    /// </summary>
    /// <param name="id">The id of the RDS with the requested state change</param>
    /// <returns>An approval data object containing the id of the RDS and the reverted state</returns>
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
            var previousState = await _logService.GetPreviousState(id, nameof(RdsLibDm));
            var hasAccess = await _authService.HasAccess(CompanyConstants.AnyCompanyId, previousState);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _rdsService.ChangeState(id, previousState);
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}