using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Tyle.Application.Common;
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
            return StatusCode(500, "Internal Server Error.");
        }
    }
}
