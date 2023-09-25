/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.TypeLibrary.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace TypeLibrary.Api.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Unit services")]
public class UnitController : ControllerBase
{
    private readonly ILogger<UnitController> _logger;
    private readonly IReferenceService _referenceService;

    public UnitController(ILogger<UnitController> logger, IReferenceService referenceService)
    {
        _logger = logger;
        _referenceService = referenceService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<UnitReference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _referenceService.GetAllUnits();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UnitReference), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get([FromRoute] int id)
    {
        try
        {
            var data = _referenceService.GetUnit(id);
            if (data == null)
                return NotFound(id);

            return Ok(data);
        }
        catch (MimirorgNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(UnitReference), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UnitReferenceRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _referenceService.CreateUnit(request);
            return Ok(cm);
        }
        catch (MimirorgBadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}*/