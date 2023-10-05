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
[SwaggerTag("Classifier services")]
public class ClassifiersController : ControllerBase
{
    private readonly IClassifierRepository _classifierRepository;


    public ClassifiersController(IClassifierRepository classifierRepository)
    {
        _classifierRepository = classifierRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RdlClassifier>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _classifierRepository.GetAll());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RdlClassifier), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var classifier = await _classifierRepository.Get(id);
            if (classifier == null)
                return NotFound();

            return Ok(classifier);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(RdlClassifier), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] RdlClassifierRequest request)
    {
        try
        {
            var classifier = await _classifierRepository.Create(request);
            return Ok(classifier);
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
            if (await _classifierRepository.Delete(id))
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