using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Terminals;

namespace Tyle.Api.Terminals;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Medium services")]
public class MediaController : ControllerBase
{
    private readonly IMediumRepository _mediumRepository;


    public MediaController(IMediumRepository mediumRepository)
    {
        _mediumRepository = mediumRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RdlMedium>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _mediumRepository.GetAll());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RdlMedium), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var medium = await _mediumRepository.Get(id);
            if (medium == null)
                return NotFound();

            return Ok(medium);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(RdlMedium), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] RdlMediumRequest request)
    {
        try
        {
            var medium = await _mediumRepository.Create(request);
            return Ok(medium);
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
            if (await _mediumRepository.Delete(id))
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