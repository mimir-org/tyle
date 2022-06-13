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
    [SwaggerTag("Library Transport Services")]
    public class LibraryTransportController : ControllerBase
    {
        private readonly ILogger<LibraryTransportController> _logger;
        private readonly ITransportService _transportService;
        private readonly ITimedHookService _hookService;

        public LibraryTransportController(ILogger<LibraryTransportController> logger, ITransportService transportService, ITimedHookService hookService)
        {
            _logger = logger;
            _transportService = transportService;
            _hookService = hookService;
        }

        /// <summary>
        /// Get transport by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TransportLibCm</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransportLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var data = await _transportService.Get(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get all transports
        /// </summary>
        /// <returns>TransportLibCm Collection</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TransportLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLatestVersions()
        {
            try
            {
                var data = await _transportService.GetLatestVersions();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Create transport
        /// </summary>
        /// <param name="dataAm"></param>
        /// <returns>TransportLibCm</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TransportLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] TransportLibAm dataAm)
        {
            try
            {
                var data = await _transportService.Create(dataAm);
                _hookService.HookQueue.Enqueue(CacheKey.Transport);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Update transport
        /// </summary>
        /// <param name="dataAm"></param>
        /// <param name="id"></param>
        /// <returns>TransportLibCm</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TransportLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> Update([FromBody] TransportLibAm dataAm, [FromRoute] string id)
        {
            try
            {
                var data = await _transportService.Update(dataAm, id);
                _hookService.HookQueue.Enqueue(CacheKey.Transport);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Delete a transport
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Delete a transport")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var data = await _transportService.Delete(id);
                _hookService.HookQueue.Enqueue(CacheKey.Transport);
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