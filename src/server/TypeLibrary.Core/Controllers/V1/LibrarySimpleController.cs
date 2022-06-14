using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [SwaggerTag("Library Simple Services")]
    public class LibrarySimpleController : ControllerBase
    {
        private readonly ILogger<LibrarySimpleController> _logger;
        private readonly ISimpleService _simpleService;

        public LibrarySimpleController(ILogger<LibrarySimpleController> logger, ISimpleService simpleService)
        {
            _logger = logger;
            _simpleService = simpleService;
        }

        /// <summary>
        /// Get simple by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SimpleLibCm</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SimpleLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSimple([FromRoute] string id)
        {
            try
            {
                var data = await _simpleService.Get(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get all simple
        /// </summary>
        /// <returns>SimpleLibCm Collection</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<SimpleLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSimple()
        {
            try
            {
                var data = await _simpleService.Get();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }
    }
}