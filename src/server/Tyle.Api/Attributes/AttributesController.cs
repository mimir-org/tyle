using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Api.Common;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Core.Common;

namespace Tyle.Api.Attributes;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Attribute services")]
public class AttributesController : ControllerBase
{
    private readonly IAttributeRepository _attributeRepository;
    private readonly IMapper _mapper;
    private readonly IApprovalService _approvalService;

    public AttributesController(IAttributeRepository attributeRepository, IMapper mapper, IApprovalService approvalService)
    {
        _attributeRepository = attributeRepository;
        _mapper = mapper;
        _approvalService = approvalService;
    }

    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>A collection of attributes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AttributeView>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] State? state = null)
    {
        try
        {
            var attributes = await _attributeRepository.GetAll(state);
            return Ok(_mapper.Map<IEnumerable<AttributeView>>(attributes));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get attribute by id
    /// </summary>
    /// <param name="id">The id of the attribute to get</param>
    /// <returns>The requested attribute</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AttributeView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var attribute = await _attributeRepository.Get(id);
            if (attribute == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AttributeView>(attribute));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Create an attribute
    /// </summary>
    /// <param name="request">The attribute that should be created</param>
    /// <returns>The created attribute</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AttributeView), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] AttributeTypeRequest request)
    {
        try
        {
            var createdAttribute = await _attributeRepository.Create(request);

            return Created("dummy", _mapper.Map<AttributeView>(createdAttribute));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update an attribute object
    /// </summary>
    /// <param name="id">The id of the attribute object that should be updated</param>
    /// <param name="request">The new values of the attribute</param>
    /// <returns>The updated attribute</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AttributeView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AttributeTypeRequest request)
    {
        try
        {
            var attribute = await _attributeRepository.Update(id, request);

            if (attribute == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AttributeView>(attribute));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Change the state of an attribute.
    /// </summary>
    /// <param name="id">The id of the attribute that will change state.</param>
    /// <param name="request">A request containing the wanted state.</param>
    [HttpPatch("{id}/state")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> ChangeState([FromRoute] Guid id, [FromBody] StateChangeRequest request)
    {
        try
        {
            // TODO: Handle authorization

            var response = await _approvalService.ChangeAttributeState(id, request.State);

            return response switch
            {
                ApprovalResponse.Accepted => NoContent(),
                ApprovalResponse.TypeNotFound => NotFound(),
                ApprovalResponse.IllegalChange => BadRequest(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete an attribute
    /// </summary>
    /// <param name="id">The id of the attribute to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            if (await _attributeRepository.Delete(id))
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