using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Predicate services")]
public class PredicateController : ControllerBase
{
    private readonly ILogger<PredicateController> _logger;
    private readonly IReferenceService _referenceService;

    public PredicateController(ILogger<PredicateController> logger, IReferenceService referenceService)
    {
        _logger = logger;
        _referenceService = referenceService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<PredicateReference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult Get()
    {
        try
        {
            var data = _referenceService.GetAllPredicates();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PredicateReference), StatusCodes.Status200OK)]
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
    [ProducesResponseType(typeof(PredicateReference), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] PredicateReferenceRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cm = await _referenceService.CreatePredicate(request);
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