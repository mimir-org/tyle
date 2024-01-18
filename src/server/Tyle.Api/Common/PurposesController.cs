using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Models.Constants;
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
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(IEnumerable<RdlPurpose>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(RdlPurpose), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}")]
    [ProducesResponseType(typeof(RdlPurpose), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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