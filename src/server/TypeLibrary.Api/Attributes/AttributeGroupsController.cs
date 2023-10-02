using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Constants;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Api.Attributes;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Attribute group services")]

public class AttributeGroupsController : ControllerBase
{
    private readonly IAttributeGroupRepository _attributeGroupRepository;
    private readonly IMapper _mapper;

    public AttributeGroupsController(IAttributeGroupRepository attributeGroupRepository, IMapper mapper)
    {
        _attributeGroupRepository = attributeGroupRepository;
        _mapper = mapper;
    }


    /// <summary>
    /// Get all attribute groups
    /// </summary>
    /// <returns>A collection of attribute groups</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AttributeGroupView>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var attributeGroups = await _attributeGroupRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<AttributeGroupView>>(attributeGroups));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get attribute group by id
    /// </summary>
    /// <param name="id">The id of the attribute group to get</param>
    /// <returns>The requested attribute group</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AttributeGroupView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var attributeGroup = await _attributeGroupRepository.Get(id);
            if (attributeGroup == null)
            {
                return NotFound(id);
            }

            return Ok(_mapper.Map<AttributeGroupView>(attributeGroup));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Create an attribute group
    /// </summary>
    /// <param name="request">The attribute group that should be created</param>
    /// <returns>The created attribute group</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AttributeGroupView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] AttributeGroupRequest request)
    {
        try
        {
            var createdAttributeGroup = await _attributeGroupRepository.Create(request);
            return Created("dummy", _mapper.Map<AttributeGroupView>(createdAttributeGroup));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update an attribute group
    /// </summary>
    /// <param name="id">The id of the attribute group object that should be updated</param>
    /// <param name="request">The new values of the attribute group</param>
    /// <returns>The updated attribute group</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AttributeGroupView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AttributeGroupRequest request)
    {
        try
        {
            var attributeGroup = await _attributeGroupRepository.Update(id, request);

            if (attributeGroup == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AttributeGroupView>(attributeGroup));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete an attribute group
    /// </summary>
    /// <param name="id">The id of the attribute group to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            if (await _attributeGroupRepository.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}