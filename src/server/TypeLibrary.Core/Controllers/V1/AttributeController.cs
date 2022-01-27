using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;
using Attribute = TypeLibrary.Models.Data.Attribute;
using PredefinedAttribute = TypeLibrary.Models.Application.PredefinedAttribute;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// Library file services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Attribute services")]
    public class AttributeController : ControllerBase
    {
        private readonly ILogger<AttributeController> _logger;
        private readonly IAttributeService _attributeService;

        public AttributeController(ILogger<AttributeController> logger, IAttributeService attributeService)
        {
            _logger = logger;
            _attributeService = attributeService;
        }

        /// <summary>
        /// Get all attributes by aspect.
        /// If aspect is NotSet, all attributes will be returned
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        [HttpGet("{aspect}")]
        [ProducesResponseType(typeof(ICollection<Attribute>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetAttributes(Aspect aspect)
        {
            try
            {
                var data = _attributeService.GetAttributes(aspect).ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get predefined attributes
        /// </summary>
        /// <returns></returns>
        [HttpGet("predefined-attributes")]
        [ProducesResponseType(typeof(ICollection<PredefinedAttribute>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetPredefinedAttributes()
        {
            try
            {
                var data = _attributeService.GetPredefinedAttributes().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="attributeAm"></param>
        /// <returns></returns>
        [HttpPost("attribute")]
        [ProducesResponseType(typeof(Attribute), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateAttribute([FromBody] AttributeAm attributeAm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var attribute = await _attributeService.CreateAttribute(attributeAm);
                if (attribute == null)
                    return BadRequest("The attribute already exist");

                return Ok(attribute);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}