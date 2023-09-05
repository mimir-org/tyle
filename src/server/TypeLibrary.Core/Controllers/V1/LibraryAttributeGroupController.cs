
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using TypeLibrary.Core.Helper;
using TypeLibrary.Data.Models;
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

    public LibraryAttributeGroupController(IAttributeService attributeService, IAttributeGroupService attributeGroupService, IMimirorgAuthService authService)
    {
        _attributeService = attributeService;
        _authService = authService;
        _attributeGroupService = attributeGroupService;
    }


    /// <summary>
    /// Get all attribute groups available
    /// </summary>
    /// <returns>A collection of attribute groups</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AttributeLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromBody] string searchText)
    {
        try
        {
            var data = _attributeGroupService.GetAttributeGroupList(searchText);
            return Ok(data);
        }
        catch (Exception e)
        {
            var logger = new ExeptionLogger();
            logger.LoggExeption(e);
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
            var logger = new ExeptionLogger();
            logger.LoggExeption(e);
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
    public async Task<IActionResult> Create([FromBody] AttributeGroupLibAm attributeGroup)
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
            var logger = new ExeptionLogger();
            logger.LoggExeption(e);
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
        //Todo add the group id and attribute change id

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
            var logger = new ExeptionLogger();
            logger.LoggExeption(e);
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
            var attribute = await _attributeGroupService.GetSingleAttributeGroup(id);
            var hasAccess = await _authService.CanDelete(attribute.State, attribute.CreatedBy, CompanyConstants.AnyCompanyId);

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
            var logger = new ExeptionLogger();
            logger.LoggExeption(e);
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Update an attribute group with a new state
    /// </summary>
    /// <param name="id">The id of the attribute group to be updated</param>
    /// <param name="state">The new state</param>
    /// <returns>An approval data object containing the id of the attribute group and the new state</returns>
    [HttpPatch("{id}/state/{state}")]
    [ProducesResponseType(typeof(ApprovalDataCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeState([FromRoute] string id, [FromRoute] State state)
    {
        try
        {
            var hasAccess = await _authService.HasAccess(CompanyConstants.AnyCompanyId, state);

            if (!hasAccess)
                return StatusCode(StatusCodes.Status403Forbidden);

            var data = await _attributeGroupService.ChangeState(id, state, true);
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
            var logger = new ExeptionLogger();
            logger.LoggExeption(e);
            return StatusCode(500, "Internal Server Error");
        }
    }

}
