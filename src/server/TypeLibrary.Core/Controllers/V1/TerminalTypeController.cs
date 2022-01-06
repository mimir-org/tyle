using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data.TypeEditor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Services.Contracts;

// ReSharper disable StringLiteralTypo

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// Terminal type services
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("TerminalType")]
    public class TerminalTypeController : ControllerBase
    {
        private readonly ILogger<TerminalTypeController> _logger;
        private readonly ITerminalTypeService _terminalTypeService;

        public TerminalTypeController(ILogger<TerminalTypeController> logger, ITerminalTypeService terminalTypeService)
        {
            _logger = logger;
            _terminalTypeService = terminalTypeService;
        }

        /// <summary>
        /// Get terminal types
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(ICollection<TerminalType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = "Read")]
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
        [ProducesResponseType(typeof(Dictionary<string, TerminalType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = "Read")]
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
        /// Create a terminal type
        /// </summary>
        /// <param name="createTerminalType"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(AttributeType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateTerminalType([FromBody] CreateTerminalType createTerminalType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdTerminalType = await _terminalTypeService.CreateTerminalType(createTerminalType);
                if (createdTerminalType == null)
                    return BadRequest("The terminal type already exist");

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
