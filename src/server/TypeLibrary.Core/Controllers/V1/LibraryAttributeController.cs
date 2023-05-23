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
[SwaggerTag("Attribute services")]
public class LibraryAttributeController : ControllerBase
{
    private readonly ILogger<LibraryAttributeController> _logger;
    private readonly IAttributeService _attributeService;
    private readonly IMimirorgAuthService _authService;

    public LibraryAttributeController(ILogger<LibraryAttributeController> logger, IAttributeService attributeService, IMimirorgAuthService authService)
    {
        _logger = logger;
        _attributeService = attributeService;
        _authService = authService;
    }

    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>A collection of attributes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AttributeLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get([FromRoute] string id)
    {
        try
        {
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
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    /// Update an attribute object
    /// </summary>
    /// <param name="id">The id of the attribute object that should be updated</param>
    /// <param name="attribute">The new values of the attribute</param>
    /// <returns>The updated attribute</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "attribute", "CompanyId")]
    public async Task<IActionResult> Update(string id, [FromBody] AttributeLibAm attribute)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _attributeService.Update(id, attribute);
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
    /// Update an attribute with a new state
    /// </summary>
    /// <param name="id">The id of the attribute to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the attribute and the new state</returns>
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

            var data = await _attributeService.ChangeState(id, state);
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
    /// <param name="id">The id of the attribute with the requested state change</param>
    /// <returns>An approval data object containing the id of the attribute and the reverted state</returns>
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
            var cm = _attributeService.Get(id);

            if (cm == null)
                return StatusCode(StatusCodes.Status404NotFound);

            if (cm.State is State.Draft or State.Deleted or State.Approved)
                return StatusCode(StatusCodes.Status403Forbidden, $"Can't reject a state change for an object with state {cm.State}");

            var hasAccess = await _authService.HasAccess(CompanyConstants.AnyCompanyId, cm.State == State.Approve ? State.Approved : State.Delete);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var data = await _attributeService.ChangeState(id, State.Draft);
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
}