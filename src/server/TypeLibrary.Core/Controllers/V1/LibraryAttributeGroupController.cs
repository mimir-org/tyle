
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
using System.Threading.Tasks;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Attribute group services")]
[Authorize]

public class LibraryAttributeGroupController : ControllerBase
{
    private readonly IAttributeService _attributeService;
    private readonly IMimirorgAuthService _authService;
    private readonly IAttributeGroupService _attributeGroupService;
    private readonly ILogger<LibraryAttributeController> _logger;

    public LibraryAttributeGroupController(IAttributeService attributeService, IAttributeGroupService attributeGroupService, IMimirorgAuthService authService, ILogger<LibraryAttributeController> logger)
    {
        _attributeService = attributeService;
        _authService = authService;
        _attributeGroupService = attributeGroupService;
        _logger = logger;
    }


    /// <summary>
    /// Get all attribute groups available
    /// </summary>
    /// <returns>A collection of attribute groups</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AttributeLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        try
        {
            var data = _attributeGroupService.GetAttributeGroupList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError("Internal server error", (e.Message, e.StackTrace, e.InnerException, e.Data, e.Source));
            return StatusCode(500, "Internal Server Error.");
        }
    }

    /// <summary>
    /// Get attributeGroup by id
    /// </summary>
    /// <param name="id">The id of the attribute to get</param>
    /// <returns>The requested attribute</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetSingleAttributeGroup([FromRoute] string id)
    {
        try
        {
            var data = _attributeGroupService.GetSingleAttributeGroup(id);
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
            _logger.LogError("Internal server error", (e.Message, e.StackTrace, e.InnerException, e.Data, e.Source));
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Create an attribute group
    /// </summary>
    /// <param name="attributeGroup">The attribute group that should be created</param>
    /// <returns>The created attribute group</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "attributeGroup", "CompanyId")]
    public async Task<IActionResult> CreateAttributeGroup([FromBody] AttributeGroupLibAm attributeGroup)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _attributeGroupService.Create(attributeGroup);

            return Ok(cm);
        }
        catch (MimirorgBadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError("Internal server error", (e.Message, e.StackTrace, e.InnerException, e.Data, e.Source));
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Update an attributeGroup and/or object in group
    /// </summary>
    /// <param name="id">The id of the attribute group object that should be updated</param>
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
    public async Task<IActionResult> Update(string id, [FromBody] AttributeGroupLibAm attribute)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _attributeGroupService.Update(id, attribute);
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
            _logger.LogError("Internal server error", (e.Message, e.StackTrace, e.InnerException, e.Data, e.Source));
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Delete an attribute goup that is not approved
    /// </summary>
    /// <param name="id">The id of the attribute group to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        try
        {
            var attribute = _attributeGroupService.GetSingleAttributeGroup(id);
            var hasAccess = await _authService.CanDelete(State.Draft, attribute.CreatedBy, CompanyConstants.AnyCompanyId); //State hardcoded

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            await _attributeGroupService.Delete(id);
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
            _logger.LogError("Internal server error", (e.Message, e.StackTrace, e.InnerException, e.Data, e.Source));
            return StatusCode(500, "Internal Server Error");
        }
    }

}