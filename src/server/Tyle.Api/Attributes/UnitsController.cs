using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Models.Constants;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;

namespace Tyle.Api.Attributes;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Unit services")]
public class UnitsController : ControllerBase
{
    private readonly IUnitRepository _unitRepository;


    public UnitsController(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    [HttpGet]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(IEnumerable<RdlUnit>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _unitRepository.GetAll());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(RdlUnit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var unit = await _unitRepository.Get(id);
            if (unit == null)
                return NotFound();

            return Ok(unit);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}")]
    [ProducesResponseType(typeof(RdlUnit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] RdlUnitRequest request)
    {
        try
        {
            var unit = await _unitRepository.Create(request);
            return Ok(unit);
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
            if (await _unitRepository.Delete(id))
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