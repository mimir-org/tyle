using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;
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
        private readonly IMimirorgUserService _userService;

        public LibraryAttributeController(ILogger<LibraryAttributeController> logger, IAttributeService attributeService, IMimirorgUserService userService)
        {
            _logger = logger;
            _attributeService = attributeService;
            _userService = userService;
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

                var cm = await _attributeService.Create(attribute, true);
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
        public async Task<IActionResult> GetAttributeById(string id)
        {
            try
            {
                var data = await _attributeService.Get(id);
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

        [HttpGet("condition")]
        [ProducesResponseType(typeof(ICollection<AttributeConditionLibCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetConditions()
        {
            try
            {
                var data = await _attributeService.GetConditions();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("format")]
        [ProducesResponseType(typeof(ICollection<AttributeFormatLibCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetFormats()
        {
            try
            {
                var data = await _attributeService.GetFormats();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("qualifier")]
        [ProducesResponseType(typeof(ICollection<AttributeQualifierLibCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetQualifiers()
        {
            try
            {
                var data = await _attributeService.GetQualifiers();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("source")]
        [ProducesResponseType(typeof(ICollection<AttributeSourceLibCm>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetSources()
        {
            try
            {
                var data = await _attributeService.GetSources();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

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

        /// <summary>
        /// Update an attribute
        /// </summary>
        /// <param name="dataAm"></param>
        /// <param name="id"></param>
        /// <returns>AttributeLibCm</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AttributeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "dataAm", "CompanyId")]
        public async Task<IActionResult> Update([FromBody] AttributeLibAm dataAm, [FromRoute] string id)
        {
            try
            {
                var companyIsChanged = await _attributeService.CompanyIsChanged(id, dataAm.CompanyId);
                if (companyIsChanged)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _attributeService.Update(dataAm, id);
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
        //TODO: *************************************
        //TODO: Set correct authorization requirement
        //TODO: *************************************
        public async Task<IActionResult> UpdateState([FromBody] State state, [FromRoute] string id)
        {
            try
            {
                var data = await _attributeService.UpdateState(id, state);
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
        /// Delete an attribute
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation("Delete an attribute")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var node = await _attributeService.Get(id);

                if (node == null)
                    return NotFound($"Can't find attribute with id: {id}");

                var currentUser = await _userService.GetUser(HttpContext.User);
                if (!currentUser.Permissions.TryGetValue(node.CompanyId, out var permission))
                    return StatusCode(StatusCodes.Status403Forbidden);

                if (!permission.HasFlag(MimirorgPermission.Delete))
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _attributeService.Delete(id);
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