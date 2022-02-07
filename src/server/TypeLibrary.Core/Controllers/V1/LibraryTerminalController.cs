using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

// ReSharper disable StringLiteralTypo

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// Terminal typeDm services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Terminal services")]
    public class LibraryTerminalController : ControllerBase
    {
        private readonly ILogger<LibraryTerminalController> _logger;
        private readonly ITerminalService _terminalTypeService;

        public LibraryTerminalController(ILogger<LibraryTerminalController> logger, ITerminalService terminalTypeService)
        {
            _logger = logger;
            _terminalTypeService = terminalTypeService;
        }

        /// <summary>
        /// Get terminal types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TerminalLibDm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetTerminalTypes()
        {
            try
            {
                var data = _terminalTypeService.GetTerminals().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get categorized terminals
        /// </summary>
        /// <returns></returns>
        [HttpGet("category")]
        [ProducesResponseType(typeof(Dictionary<string, TerminalLibDm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetTerminalTypesByCategory()
        {
            try
            {
                var data = _terminalTypeService.GetTerminalsByCategory().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Create a terminal typeDm
        /// </summary>
        /// <param name="terminalAm"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AttributeLibDm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateTerminalType([FromBody] TerminalLibAm terminalAm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdTerminalType = await _terminalTypeService.CreateTerminalType(terminalAm);
                if (createdTerminalType == null)
                    return BadRequest("The terminal typeDm already exist");

                return Ok(createdTerminalType);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}