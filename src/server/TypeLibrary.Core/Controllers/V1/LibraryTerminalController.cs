using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
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
        [ProducesResponseType(typeof(ICollection<TerminalLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetTerminals()
        {
            try
            {
                var data = _terminalTypeService.Get().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}