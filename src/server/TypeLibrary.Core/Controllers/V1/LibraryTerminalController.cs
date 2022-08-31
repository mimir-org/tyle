using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Terminal services")]
    public class LibraryTerminalController : ControllerBase
    {
        private readonly ILogger<LibraryTerminalController> _logger;
        private readonly ITerminalService _terminalService;

        public LibraryTerminalController(ILogger<LibraryTerminalController> logger, ITerminalService terminalService)
        {
            _logger = logger;
            _terminalService = terminalService;
        }

        /// <summary>
        /// Get terminal types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TerminalLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult GetTerminals()
        {
            try
            {
                var data = _terminalService.Get().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Create a terminal
        /// </summary>
        /// <param name="terminal">The terminal that should be created</param>
        /// <returns>The created terminal</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "terminal", "CompanyId")]
        public async Task<IActionResult> Create([FromBody] TerminalLibAm terminal)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cm = await _terminalService.Create(terminal);
                return Ok(cm);
            }
            catch (MimirorgBadRequestException e)
            {
                _logger.LogWarning(e, $"Warning error: {e.Message}");

                foreach (var error in e.Errors().ToList())
                {
                    ModelState.Remove(error.Key);
                    ModelState.TryAddModelError(error.Key, error.Error);
                }

                return BadRequest(ModelState);
            }
            catch (MimirorgDuplicateException e)
            {
                ModelState.Remove("Id");
                ModelState.TryAddModelError("Id", e.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }
    }
}