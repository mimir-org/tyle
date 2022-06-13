using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
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
        private readonly ITimedHookService _hookService;

        public LibraryInterfaceController(ILogger<LibraryInterfaceController> logger, IInterfaceService interfaceService, ITimedHookService hookService)
        {
            _logger = logger;
            _interfaceService = interfaceService;
            _hookService = hookService;
        }

        /// <summary>
        /// Get interface by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>InterfaceLibCm</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InterfaceLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var data = await _interfaceService.Get(id);
                return Ok(data);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] InterfaceLibAm dataAm)
        {
            try
            {
                var data = await _interfaceService.Create(dataAm);
                _hookService.HookQueue.Enqueue(CacheKey.Interface);
                return Ok(data);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> Update([FromBody] InterfaceLibAm dataAm, [FromRoute] string id)
        {
            try
            {
                var data = await _interfaceService.Update(dataAm, id);
                _hookService.HookQueue.Enqueue(CacheKey.Interface);
                return Ok(data);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        [SwaggerOperation("Delete an interface")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var data = await _interfaceService.Delete(id);
                _hookService.HookQueue.Enqueue(CacheKey.Interface);
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