using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Attribute services")]
    public class LibraryAttributeController : ControllerBase
    {
        private readonly ILogger<LibraryAttributeController> _logger;
        private readonly IAttributeService _attributeService;

        public LibraryAttributeController(ILogger<LibraryAttributeController> logger, IAttributeService attributeService)
        {
            _logger = logger;
            _attributeService = attributeService;
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>The created attribute</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "attribute", "CompanyId")]
        public async Task<IActionResult> Create([FromBody] AttributeLibAm attribute)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cm = await _attributeService.Create(attribute);
                return Ok(cm);
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
            catch (MimirorgDuplicateException e)
            {
                ModelState.Remove("Id");
                ModelState.TryAddModelError("Id", e.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<AttributeLibCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                var data = _attributeService.GetLatestVersions(Aspect.NotSet);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public IActionResult GetAttributeById(string id)
        {
            try
            {
                var data = _attributeService.GetLatestVersion(id);
                if (data == null)
                    return NotFound();

                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("aspect/{aspect}")]
        [ProducesResponseType(typeof(ICollection<AttributeLibCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetLatestVersions(Aspect aspect)
        {
            try
            {
                var data = _attributeService.GetLatestVersions(aspect);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("predefined")]
        [ProducesResponseType(typeof(ICollection<AttributePredefinedLibCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetPredefined()
        {
            try
            {
                var data = _attributeService.GetPredefined().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Update an attribute
        /// </summary>
        /// <param name="attributeAm"></param>
        /// <returns>AttributeLibCm</returns>
        [HttpPut]
        [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "attributeAm", "CompanyId")]
        public async Task<IActionResult> Update([FromBody] AttributeLibAm attributeAm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var companyId = await _attributeService.GetCompanyId(attributeAm.Id);

                if (companyId != attributeAm.CompanyId)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _attributeService.Update(attributeAm);
                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
                foreach (var error in e.Errors().ToList())
                {
                    ModelState.Remove(error.Key);
                    ModelState.TryAddModelError(error.Key, error.Error);
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Update attribute with new state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="id"></param>
        /// <returns>AttributeLibCm</returns>
        [HttpPatch("state/{id}")]
        [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> ChangeState([FromBody] State state, [FromRoute] string id)
        {
            try
            {
                var data = await _attributeService.ChangeState(id, state);
                return Ok(data);
            }
            catch (MimirorgBadRequestException e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get all datums of a given type
        /// </summary>
        /// <param name="type">The type of the quantity datum you want to receive</param>
        /// <returns>A collection of quantity datums</returns>
        [HttpGet("datum/{type}")]
        [ProducesResponseType(typeof(ICollection<QuantityDatumCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> GetDatums(QuantityDatumType type)
        {
            try
            {
                var data = type switch
                {
                    QuantityDatumType.QuantityDatumRangeSpecifying => await _attributeService
                        .GetQuantityDatumRangeSpecifying(),
                    QuantityDatumType.QuantityDatumRegularitySpecified => await _attributeService
                        .GetQuantityDatumRegularitySpecified(),
                    QuantityDatumType.QuantityDatumSpecifiedProvenance => await _attributeService
                        .GetQuantityDatumSpecifiedProvenance(),
                    QuantityDatumType.QuantityDatumSpecifiedScope => await _attributeService
                        .GetQuantityDatumSpecifiedScope(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Enum type ({nameof(QuantityDatumType)}): {type} is out of range.")
                };

                return Ok(data?.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get all attribute type references
        /// </summary>
        /// <returns>A collection of references></returns>
        [HttpGet("reference")]
        [ProducesResponseType(typeof(ICollection<TypeReferenceCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetReferences()
        {
            try
            {
                var data = (await _attributeService.GetAttributeReferences()).ToList();
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