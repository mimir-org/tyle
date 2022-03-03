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
    [SwaggerTag("Format services")]
    public class LibraryAttributeFormatController : ControllerBase
    {
        private readonly ILogger<LibraryAttributeFormatController> _logger;
        private readonly IAttributeFormatService _attributeFormatService;

        public LibraryAttributeFormatController(ILogger<LibraryAttributeFormatController> logger, IAttributeFormatService attributeFormatService)
        {
            _logger = logger;
            _attributeFormatService = attributeFormatService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<AttributeFormatLibAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetFormats()
        {
            try
            {
                var data = await _attributeFormatService.GetAttributeFormats();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AttributeFormatLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateFormat([FromBody] AttributeFormatLibAm dataAm, [FromRoute] string id)
        {
            try
            {
                var data = await _attributeFormatService.UpdateAttributeFormat(dataAm, id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AttributeFormatLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateFormat([FromBody] AttributeFormatLibAm dataAm)
        {
            try
            {
                var data = await _attributeFormatService.CreateAttributeFormat(dataAm);
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