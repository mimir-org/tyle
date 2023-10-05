using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Api.Common;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Purpose services")]
public class PurposesController : ControllerBase
{
    private readonly IPurposeRepository _purposeRepository;


    public PurposesController(IPurposeRepository purposeRepository)
    {
        _purposeRepository = purposeRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RdlPurpose>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _purposeRepository.GetAll());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RdlPurpose), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var purpose = await _purposeRepository.Get(id);
            if (purpose == null)
                return NotFound();

            return Ok(purpose);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(RdlPurpose), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] RdlPurposeRequest request)
    {
        try
        {
            var purpose = await _purposeRepository.Create(request);
            return Ok(purpose);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            if (await _purposeRepository.Delete(id))
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