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
    [SwaggerTag("Source services")]
    public class LibraryAttributeSourceController : ControllerBase
    {
        private readonly ILogger<LibraryAttributeSourceController> _logger;
        private readonly IAttributeSourceService _attributeSourceService;

        public LibraryAttributeSourceController(ILogger<LibraryAttributeSourceController> logger, IAttributeSourceService attributeSourceService)
        {
            _logger = logger;
            _attributeSourceService = attributeSourceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<AttributeSourceLibAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetSources()
        {
            try
            {
                var data = await _attributeSourceService.GetAttributeSources();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(AttributeSourceLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateSource([FromBody] AttributeSourceLibAm dataAm)
        {
            try
            {
                var data = await _attributeSourceService.UpdateAttributeSource(dataAm);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AttributeSourceLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateSource([FromBody] AttributeSourceLibAm dataAm)
        {
            try
            {
                var data = await _attributeSourceService.CreateAttributeSource(dataAm);
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