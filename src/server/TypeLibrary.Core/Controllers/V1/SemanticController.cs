using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// TypeCm file services
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/ont")]
    [SwaggerTag("Ontology Services")]
    public class SemanticController : ControllerBase
    {
        private readonly ILogger<SemanticController> _logger;
        private readonly IAttributeService _attributeService;

        public SemanticController(ILogger<SemanticController> logger, IAttributeService attributeService)
        {
            _logger = logger;
            _attributeService = attributeService;
        }

        /// <summary>
        /// Get attribute ontology
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("attribute/{id}")]
        [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAttribute(string id)
        {
            try
            {
                var data = _attributeService.Get(Aspect.NotSet).FirstOrDefault(x => x.Id == id);
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