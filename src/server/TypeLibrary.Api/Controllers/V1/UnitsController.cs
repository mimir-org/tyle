using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Api.Controllers.V1;

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
    [ProducesResponseType(typeof(IEnumerable<RdlUnit>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[AllowAnonymous]
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
    [ProducesResponseType(typeof(RdlUnit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[AllowAnonymous]
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
    [ProducesResponseType(typeof(RdlUnit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[AllowAnonymous]
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
}