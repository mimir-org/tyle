using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
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
        private readonly INodeService _nodeService;
        private readonly ITerminalService _terminalService;
        private readonly IUnitService _unitService;
        private readonly MimirorgAuthSettings _authSettings;

        public SemanticController(ILogger<SemanticController> logger, IAttributeService attributeService, INodeService nodeService, ITerminalService terminalService, IUnitService unitService, IOptions<MimirorgAuthSettings> authSettings)
        {
            _logger = logger;
            _attributeService = attributeService;
            _nodeService = nodeService;
            _terminalService = terminalService;
            _unitService = unitService;
            _authSettings = authSettings?.Value;
        }

        /// <summary>
        /// Get node ontology
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // ReSharper disable once StringLiteralTypo
        [HttpGet("aspectnode/{id}")]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult GetNode(string id)
        {
            try
            {
                var data = _nodeService.GetLatestVersion(id);
                if (data == null)
                    return NoContent();

                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
                _logger.LogWarning(e, $"Warning error: {e.Message}");

                foreach (var error in e.Errors().ToList())
                {
                    ModelState.Remove(error.Key);
                    ModelState.TryAddModelError(error.Key, error.Error);
                }

                return BadRequest(ModelState);
            }
            catch (MimirorgNotFoundException)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get attribute ontology
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("attribute/{id}")]
        [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult GetAttribute(string id)
        {
            try
            {
                var data = _attributeService.GetLatestVersions(Aspect.NotSet).FirstOrDefault(x => x.Id == id);
                if (data == null)
                    return NoContent();

                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
                _logger.LogWarning(e, $"Warning error: {e.Message}");

                foreach (var error in e.Errors().ToList())
                {
                    ModelState.Remove(error.Key);
                    ModelState.TryAddModelError(error.Key, error.Error);
                }

                return BadRequest(ModelState);
            }
            catch (MimirorgNotFoundException)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get terminal ontology
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("terminal/{id}")]
        [ProducesResponseType(typeof(TerminalLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult GetTerminal(string id)
        {
            try
            {
                var data = _terminalService.GetLatestVersions().FirstOrDefault(x => x.Id == id);
                if (data == null)
                    return NoContent();

                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
                _logger.LogWarning(e, $"Warning error: {e.Message}");

                foreach (var error in e.Errors().ToList())
                {
                    ModelState.Remove(error.Key);
                    ModelState.TryAddModelError(error.Key, error.Error);
                }

                return BadRequest(ModelState);
            }
            catch (MimirorgNotFoundException)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get unit ontology
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("unit/{id}")]
        [ProducesResponseType(typeof(UnitLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> GetUnit(string id)
        {
            try
            {
                var data = await _unitService.Get(id);
                if (data == null)
                    return NoContent();

                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
                _logger.LogWarning(e, $"Warning error: {e.Message}");

                foreach (var error in e.Errors().ToList())
                {
                    ModelState.Remove(error.Key);
                    ModelState.TryAddModelError(error.Key, error.Error);
                }

                return BadRequest(ModelState);
            }
            catch (MimirorgNotFoundException)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}