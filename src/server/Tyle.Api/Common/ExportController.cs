using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Application.Attributes;
using Tyle.Export;

namespace Tyle.Api.Common;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Export services")]
public class ExportController : ControllerBase
{
    private readonly IAttributeRepository _attributeRepository;

    public ExportController(IAttributeRepository attributeRepository)
    {
        _attributeRepository = attributeRepository;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var attribute = await _attributeRepository.Get(id);

            if (attribute == null)
            {
                return Ok("");
            }

            return Ok(JsonLdExport.ConvertAttributeType(attribute));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}