using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
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
        private readonly IMimirorgUserService _userService;

        public LibraryTerminalController(ILogger<LibraryTerminalController> logger, ITerminalService terminalService, IMimirorgUserService userService)
        {
            _logger = logger;
            _terminalService = terminalService;
            _userService = userService;
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
                var data = _terminalService.GetLatestVersions().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get terminal type
        /// </summary>
        /// <param name="id">The id of the terminal that should be returned</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult GetTerminal(string id)
        {
            try
            {
                var data = _terminalService.GetLatestVersion(id);
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

                var cm = await _terminalService.Create(terminal, true);
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

        /// <summary>
        /// Update terminal
        /// </summary>
        /// <param name="terminal">The terminal that should be updated</param>
        /// <param name="id">The id of the terminal that should be updated</param>
        /// <returns>TerminalLibCm</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [MimirorgAuthorize(MimirorgPermission.Write, "terminal", "CompanyId")]
        public async Task<IActionResult> Update([FromBody] TerminalLibAm terminal, [FromRoute] string id)
        {
            try
            {
                var companyIsChanged = await _terminalService.CompanyIsChanged(terminal.Id, terminal.CompanyId);
                if (companyIsChanged)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _terminalService.Update(terminal);
                return Ok(data);
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
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Update terminal with new state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="id"></param>
        /// <returns>TerminalLibCm</returns>
        [HttpPatch("state/{id}")]
        [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //TODO: *************************************
        //TODO: Set correct authorization requirement
        //TODO: *************************************
        public async Task<IActionResult> UpdateState([FromBody] State state, [FromRoute] string id)
        {
            try
            {
                var data = await _terminalService.UpdateState(id, state);
                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Delete a terminal
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation("Delete a terminal")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var terminalLibCm = _terminalService.GetLatestVersion(id);

                if (terminalLibCm == null)
                    return NotFound($"Can't find terminal with id: {id}");

                var currentUser = await _userService.GetUser(HttpContext.User);
                if (!currentUser.Permissions.TryGetValue(terminalLibCm.CompanyId, out var permission))
                    return StatusCode(StatusCodes.Status403Forbidden);

                if (!permission.HasFlag(MimirorgPermission.Delete))
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _terminalService.Delete(id);
                return Ok(data);
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
            catch (MimirorgNotFoundException)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}