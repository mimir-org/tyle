using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// TypeCm file services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("AttributeType services")]
    public class LibraryAttributeTypeController : ControllerBase
    {
        private readonly ILogger<LibraryAttributeTypeController> _logger;
        private readonly IAttributeTypeService _attributeTypeService;

        public LibraryAttributeTypeController(ILogger<LibraryAttributeTypeController> logger, IAttributeTypeService attributeTypeService)
        {
            _logger = logger;
            _attributeTypeService = attributeTypeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<AttributeTypeLibAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetAttributeTypes()
        {
            try
            {
                var data = await _attributeTypeService.GetAttributeTypes();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(AttributeTypeLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateAttributeType([FromBody] AttributeTypeLibAm dataAm)
        {
            try
            {
                var data = await _attributeTypeService.UpdateAttributeType(dataAm);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AttributeTypeLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateAttributeType([FromBody] AttributeTypeLibAm dataAm)
        {
            try
            {
                var data = await _attributeTypeService.CreateAttributeType(dataAm);
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