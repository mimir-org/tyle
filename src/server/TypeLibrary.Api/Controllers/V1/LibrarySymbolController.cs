using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Api.Controllers.V1;

/// <summary>
/// TypeCm file services
/// </summary>
[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Symbol services")]
public class LibrarySymbolController : ControllerBase
{
    private readonly ILogger<LibrarySymbolController> _logger;
    private readonly ISymbolService _symbolService;

    public LibrarySymbolController(ILogger<LibrarySymbolController> logger, ISymbolService symbolService)
    {
        _logger = logger;
        _symbolService = symbolService;
    }

    /// <summary>
    /// Get symbol
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<SymbolLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult GetSymbol()
    {
        try
        {
            var data = _symbolService.Get().ToList();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}