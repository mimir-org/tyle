using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
[ApiController]
public class ClassifiersController : ControllerBase
{
    private readonly IReferenceService _referenceService;

    public ClassifiersController(IReferenceService referenceService)
    {
        _referenceService = referenceService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClassifierReference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var classifiers = await _referenceService.GetAllClassifiers();
            return Ok(classifiers);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClassifierReference), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var classifier = await _referenceService.GetClassifier(id);
            if (classifier == null)
            {
                return NotFound();
            }
            return Ok(classifier);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClassifierReference), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] ClassifierReferenceRequest request)
    {
        try
        {
            return Ok(await _referenceService.CreateClassifier(request));
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
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _referenceService.DeleteClassifier(id);
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
