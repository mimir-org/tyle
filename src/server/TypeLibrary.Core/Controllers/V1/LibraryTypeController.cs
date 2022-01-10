using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Application.TypeEditor;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Services.Contracts;

// ReSharper disable StringLiteralTypo
namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// Library type services
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("LibraryType")]
    public class LibraryTypeController : ControllerBase
    {
        private readonly ILogger<LibraryTypeController> _logger;
        private readonly ILibraryTypeService _libraryTypeService;

        public LibraryTypeController(ILogger<LibraryTypeController> logger, ILibraryTypeService libraryTypeService)
        {
            _logger = logger;
            _libraryTypeService = libraryTypeService;
        }

        #region Get

        /// <summary>
        /// Get all library types
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(ICollection<LibraryType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = "Read")]
        public IActionResult GetAllLibraryTypes()
        {
            try
            {
                var allTypes = _libraryTypeService.GetAllTypes().ToList();
                return Ok(allTypes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get CreateLibraryType from LibraryTypeId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("{id}/{filter}")]
        [ProducesResponseType(typeof(CreateLibraryType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> CreateLibraryType([Required] string id, [Required] LibraryFilter filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var data = await _libraryTypeService.ConvertToCreateLibraryType(id, filter);
                return Ok(data);
            }
            catch (ModelBuilderNotFoundException e)
            {
                ModelState.AddModelError("Not found", e.Message);
                return BadRequest(ModelState);
            }
            catch (ModelBuilderInvalidOperationException e)
            {
                ModelState.AddModelError("Invalid value", e.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get all simple types
        /// </summary>
        /// <returns></returns>
        [HttpGet("simpleType")]
        [ProducesResponseType(typeof(IEnumerable<SimpleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "Read")]
        public IActionResult GetSimpleTypes()
        {
            try
            {
                var types = _libraryTypeService.GetSimpleTypes().ToList();
                return Ok(types);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion

        #region Post

        /// <summary>
        /// Create a library type
        /// </summary>
        /// <param name="libraryType"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(LibraryType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateLibraryType([FromBody] CreateLibraryType libraryType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (libraryType.Aspect == Aspect.Location)
                {
                    libraryType.ObjectType = ObjectType.ObjectBlock;
                }

                switch (libraryType.ObjectType)
                {
                    case ObjectType.ObjectBlock:
                        var ob = await _libraryTypeService.CreateLibraryType<LibraryNodeItem>(libraryType);
                        return Ok(ob);
                    case ObjectType.Transport:
                        var ln = await _libraryTypeService.CreateLibraryType<LibraryTransportItem>(libraryType);
                        return Ok(ln);
                    case ObjectType.Interface:
                        var libraryInterfaceItem =
                            await _libraryTypeService.CreateLibraryType<LibraryInterfaceItem>(libraryType);
                        return Ok(libraryInterfaceItem);
                    default:
                        throw new ModelBuilderInvalidOperationException(
                            $"Can't create type of: {libraryType.ObjectType}");
                }
            }
            catch (ModelBuilderDuplicateException e)
            {
                ModelState.AddModelError("Duplicate", e.Message);
                return BadRequest(ModelState);
            }
            catch (ModelBuilderNullReferenceException e)
            {
                ModelState.AddModelError("Duplicate", e.Message);
                return BadRequest(ModelState);
            }
            catch (ModelBuilderInvalidOperationException e)
            {
                ModelState.AddModelError("Duplicate", e.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Update a library type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libraryType"></param>
        /// <param name="updateMajorVersion"></param>
        /// <param name="updateMinorVersion"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(LibraryType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateLibraryType(string id, [FromBody] CreateLibraryType libraryType, bool updateMajorVersion = false, bool updateMinorVersion = false)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                switch (libraryType.ObjectType)
                {
                    case ObjectType.ObjectBlock:
                        var ob = await _libraryTypeService.UpdateLibraryType<LibraryNodeItem>(id, libraryType, updateMajorVersion, updateMinorVersion);
                        return Ok(ob);
                    case ObjectType.Transport:
                        var ln = await _libraryTypeService.UpdateLibraryType<LibraryTransportItem>(id, libraryType, updateMajorVersion, updateMinorVersion);
                        return Ok(ln);
                    case ObjectType.Interface:
                        var libraryInterfaceItem =
                            await _libraryTypeService.UpdateLibraryType<LibraryInterfaceItem>(id, libraryType, updateMajorVersion, updateMinorVersion);
                        return Ok(libraryInterfaceItem);
                    default:
                        throw new ModelBuilderInvalidOperationException(
                            $"Can't create type of: {libraryType.ObjectType}");
                }
            }
            catch (ModelBuilderNullReferenceException e)
            {
                ModelState.AddModelError("Bad request", e.Message);
                return BadRequest(ModelState);
            }
            catch (ModelBuilderNotFoundException e)
            {
                ModelState.AddModelError("Bad request", e.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Create a simple type
        /// </summary>
        /// <returns></returns>
        [HttpPost("simpleType")]
        [ProducesResponseType(typeof(IEnumerable<LibraryType>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateSimpleType(SimpleTypeAm simpleType)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdType = await _libraryTypeService.CreateSimpleType(simpleType);
                return StatusCode(201, createdType);
            }
            catch (ModelBuilderDuplicateException e)
            {
                ModelState.AddModelError("Duplicate", e.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete a type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteType(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("The id could not be null or empty");

            try
            {
                await _libraryTypeService.DeleteType(id);
                return Ok(true);
            }
            catch (ModelBuilderNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
    }
}
