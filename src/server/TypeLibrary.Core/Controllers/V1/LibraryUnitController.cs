using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Core.Controllers.V1;

/// <summary>
/// TypeCm file services
/// </summary>
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Unit services")]
public class LibraryUnitController : ControllerBase
{
    private readonly ILogger<LibraryUnitController> _logger;
    private readonly IUnitService _unitService;

    public LibraryUnitController(ILogger<LibraryUnitController> logger, IUnitService unitService)
    {
        _logger = logger;
        _unitService = unitService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<UnitLibCm>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public IActionResult GetUnits()
    {
        try
        {
            var data = _unitService.Get();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(UnitLibCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MimirorgAuthorize(MimirorgPermission.Write, "unit", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] UnitLibAm unit)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _unitService.Create(unit);
            return Ok(cm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }
}