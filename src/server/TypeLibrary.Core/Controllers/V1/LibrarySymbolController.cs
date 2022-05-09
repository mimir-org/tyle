using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// TypeCm file services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
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
        [ProducesResponseType(typeof(ICollection<SymbolLibCm>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetSymbol()
        {
            try
            {
                var data = _symbolService.GetSymbol().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Create a new symbol
        /// </summary>
        /// <param name="symbolLibAm"></param>
        /// <returns>SymbolLibCm</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SymbolLibCm), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateSymbol([FromBody] SymbolLibAm symbolLibAm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var data = await _symbolService.CreateSymbol(symbolLibAm);
                return StatusCode(201, data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}