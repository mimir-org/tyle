using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Predicate services")]
public class PredicateController : ControllerBase
{
    private readonly ILogger<UnitController> _logger;
    private readonly IReferenceService _referenceService;
    private readonly IMimirorgAuthService _authService;

    public PredicateController(ILogger<UnitController> logger, IReferenceService referenceService, IMimirorgAuthService authService)
    {
        _logger = logger;
        _referenceService = referenceService;
        _authService = authService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<PredicateReferenceCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _referenceService.GetAllPredicates().ToList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PredicateReferenceCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get([FromRoute] int id)
    {
        try
        {
            var data = _referenceService.GetPredicate(id);
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
    [ProducesResponseType(typeof(PredicateReferenceCm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] PredicateReferenceAm predicateAm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _referenceService.CreatePredicate(predicateAm);
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
}