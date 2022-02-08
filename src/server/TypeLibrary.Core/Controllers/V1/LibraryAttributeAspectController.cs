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
    [SwaggerTag("AttributeAspect services")]
    public class LibraryAttributeAspectController : ControllerBase
    {
        private readonly ILogger<LibraryAttributeAspectController> _logger;
        private readonly IAttributeAspectService _attributeAspectService;

        public LibraryAttributeAspectController(ILogger<LibraryAttributeAspectController> logger, IAttributeAspectService attributeAspectService)
        {
            _logger = logger;
            _attributeAspectService = attributeAspectService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<AttributeAspectLibAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetAttributeTypes()
        {
            try
            {
                var data = await _attributeAspectService.GetAttributeAspects();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(AttributeAspectLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateAttributeType([FromBody] AttributeAspectLibAm dataAm)
        {
            try
            {
                var data = await _attributeAspectService.UpdateAttributeAspect(dataAm);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AttributeAspectLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateAttributeType([FromBody] AttributeAspectLibAm dataAm)
        {
            try
            {
                var data = await _attributeAspectService.CreateAttributeAspect(dataAm);
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