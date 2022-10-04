using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Enums;
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

        public LibraryInterfaceController(ILogger<LibraryInterfaceController> logger, IInterfaceService interfaceService)
        {
            _logger = logger;
            _interfaceService = interfaceService;
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
        public IActionResult GetLatestVersion([FromRoute] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = _interfaceService.GetLatestVersion(id);

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
        public IActionResult GetLatestVersions()
        {
            try
            {
                var cm = _interfaceService.GetLatestVersions().ToList();
                return Ok(cm);
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
        /// <param name="interfaceAm"></param>
        /// <returns>InterfaceLibCm</returns>
        [HttpPost]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "interfaceAm", "CompanyId")]
        public async Task<IActionResult> Create([FromBody] InterfaceLibAm interfaceAm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cm = await _interfaceService.Create(interfaceAm);
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
        /// Update interface
        /// </summary>
        /// <param name="interfaceAm"></param>
        /// <returns>InterfaceLibCm</returns>
        [HttpPut]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "interfaceAm", "CompanyId")]
        public async Task<IActionResult> Update([FromBody] InterfaceLibAm interfaceAm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var companyId = await _interfaceService.GetCompanyId(interfaceAm.Id);

                if (companyId != interfaceAm.CompanyId)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _interfaceService.Update(interfaceAm);
                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
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
        [HttpPatch("{id}/state/{state}")]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> ChangeState([FromRoute] string id, [FromRoute] State state)
        {
            try
            {
                var data = await _interfaceService.ChangeState(id, state);
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
    }
}