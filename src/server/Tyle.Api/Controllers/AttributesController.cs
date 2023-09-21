using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
[ApiController]
public class AttributesController : ControllerBase
{
    private readonly IAttributeService _attributeService;

    public AttributesController(IAttributeService attributeService)
    {
        _attributeService = attributeService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AttributeType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var attributes = await _attributeService.GetAll();
            return Ok(attributes);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AttributeType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var attribute = await _attributeService.Get(id);
            if (attribute == null)
            {
                return NotFound();
            }
            return Ok(attribute);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(AttributeType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AttributeTypeRequest request)
    {
        try
        {
            return Ok(await _attributeService.Create(request));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AttributeType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AttributeTypeRequest request)
    {
        try
        {
            return Ok(await _attributeService.Update(id, request));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _attributeService.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }
}
