using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Application.TypeEditor;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{

    /// <summary>
    /// Common services
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Enum")]
    public class EnumController : ControllerBase
    {
        private readonly IEnumService _enumService;
        private readonly ILogger<EnumController> _logger;
        public EnumController(IEnumService enumService, ILogger<EnumController> logger)
        {
            _enumService = enumService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new enum
        /// </summary>
        /// <param name="createEnum"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(EnumBase), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateNewEnum([FromBody] CreateEnum createEnum)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdEnum = await _enumService.CreateEnum(createEnum);
                return StatusCode(201, createdEnum);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get all enums of given type
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        [HttpGet("{enumType}")]
        [ProducesResponseType(typeof(IEnumerable<EnumBase>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "Read")]
        public IActionResult GetAllEnumsOfType([FromRoute]EnumType enumType)
        {
            try
            {
                var data = _enumService.GetAllOfType(enumType);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get all enums - location types
        /// </summary>
        /// <returns></returns>
        [HttpGet("location-types")]
        [ProducesResponseType(typeof(IEnumerable<LocationTypeAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "Read")]
        public IActionResult GetLocationTypes()
        {
            try
            {
                var data = _enumService.GetAllLocationTypes().ToList();
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
