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

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// TypeDm services
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Library Interface Services")]
    public class LibraryInterfaceController : ControllerBase
    {
        private readonly ILogger<LibraryInterfaceController> _logger;
        private readonly IInterfaceService _interfaceService;
        private readonly IMimirorgUserService _userService;

        public LibraryInterfaceController(ILogger<LibraryInterfaceController> logger, IInterfaceService interfaceService, IMimirorgUserService userService)
        {
            _logger = logger;
            _interfaceService = interfaceService;
            _userService = userService;
        }

        /// <summary>
        /// Get interface by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>InterfaceLibCm</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var data = await _interfaceService.Get(id);
                return Ok(data);
            }
            catch (MimirorgNotFoundException)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get all interfaces
        /// </summary>
        /// <returns>InterfaceLibCm Collection</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<InterfaceLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestVersions()
        {
            try
            {
                var data = await _interfaceService.GetLatestVersions();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Create interface
        /// </summary>
        /// <param name="dataAm"></param>
        /// <returns>InterfaceLibCm</returns>
        [HttpPost]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "dataAm", "CompanyId")]
        public async Task<IActionResult> Create([FromBody] InterfaceLibAm dataAm)
        {
            try
            {
                var data = await _interfaceService.Create(dataAm, true);
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
        /// Update interface
        /// </summary>
        /// <param name="dataAm"></param>
        /// <param name="id"></param>
        /// <returns>InterfaceLibCm</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "dataAm", "CompanyId")]
        public async Task<IActionResult> Update([FromBody] InterfaceLibAm dataAm, [FromRoute] string id)
        {
            try
            {
                var companyIsChanged = await _interfaceService.CompanyIsChanged(id, dataAm.CompanyId);
                if (companyIsChanged)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _interfaceService.Update(dataAm, id);
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
        /// Update interface with new state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="id"></param>
        /// <returns>InterfaceLibCm</returns>
        [HttpPatch("state/{id}")]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
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
                var data = await _interfaceService.UpdateState(id, state);
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
        /// Delete an interface
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation("Delete an interface")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var node = await _interfaceService.Get(id);

                if (node == null)
                    return NotFound($"Can't find interface with id: {id}");

                var currentUser = await _userService.GetUser(HttpContext.User);
                if (!currentUser.Permissions.TryGetValue(node.CompanyId, out var permission))
                    return StatusCode(StatusCodes.Status403Forbidden);

                if (!permission.HasFlag(MimirorgPermission.Delete))
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _interfaceService.Delete(id);
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