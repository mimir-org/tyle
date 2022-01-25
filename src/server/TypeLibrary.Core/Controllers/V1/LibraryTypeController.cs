using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

// ReSharper disable StringLiteralTypo
namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// Library type services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
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

        #region LibraryTypes

        /// <summary>
        /// Get all library data by search
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Library), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetLibraryTypes(string name)
        {
            try
            {
                var library = await _libraryTypeService.GetLibraryTypes(name);
                return Ok(library);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
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
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> CreateLibraryType([Required] string id, [Required] LibraryFilter filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var data = await _libraryTypeService.ConvertToCreateLibraryType(id, filter);
                return Ok(data);
            }
            catch (MimirorgNotFoundException e)
            {
                ModelState.AddModelError("Not found", e.Message);
                return BadRequest(ModelState);
            }
            catch (MimirorgInvalidOperationException e)
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
        /// Create a library type
        /// </summary>
        /// <param name="libraryType"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(LibraryType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Edit")]
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
                        throw new MimirorgInvalidOperationException(
                            $"Can't create type of: {libraryType.ObjectType}");
                }
            }
            catch (MimirorgDuplicateException e)
            {
                ModelState.AddModelError("Duplicate", e.Message);
                return BadRequest(ModelState);
            }
            catch (MimirorgNullReferenceException e)
            {
                ModelState.AddModelError("Duplicate", e.Message);
                return BadRequest(ModelState);
            }
            catch (MimirorgInvalidOperationException e)
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
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateLibraryType(string id, [FromBody] CreateLibraryType libraryType,
            bool updateMajorVersion = false, bool updateMinorVersion = false)
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
                        throw new MimirorgInvalidOperationException(
                            $"Can't create type of: {libraryType.ObjectType}");
                }
            }
            catch (MimirorgNullReferenceException e)
            {
                ModelState.AddModelError("Bad request", e.Message);
                return BadRequest(ModelState);
            }
            catch (MimirorgNotFoundException e)
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
        /// Delete a type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteLibraryTypes(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("The id could not be null or empty");

            try
            {
                await _libraryTypeService.DeleteLibraryType(id);
                return Ok(true);
            }
            catch (MimirorgNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion LibraryTypes


        #region SubProject

        /// <summary>
        /// Get subProjects
        /// </summary>
        /// <returns></returns>
        [HttpGet("subProject")]
        [ProducesResponseType(typeof(ICollection<LibrarySubProjectItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetSubProjects()
        {
            try
            {
                var subProjects = await _libraryTypeService.GetSubProjects();
                return Ok(subProjects.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion SubProject


        #region NodeType

        /// <summary>
        /// Get all node types
        /// </summary>
        /// <returns></returns>
        [HttpGet("node")]
        [ProducesResponseType(typeof(ICollection<LibraryNodeItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetNodeTypes()
        {
            try
            {
                var data = await _libraryTypeService.GetNodeTypes();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion NodeType


        #region TransportType

        /// <summary>
        /// Get transport types
        /// </summary>
        /// <returns></returns>
        [HttpGet("transport")]
        [ProducesResponseType(typeof(ICollection<LibraryTransportItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetTransportTypes()
        {
            try
            {
                var transportTypes = await _libraryTypeService.GetTransportTypes();
                return Ok(transportTypes.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion TransportType


        #region InterfaceType

        /// <summary>
        /// Get interface types
        /// </summary>
        /// <returns></returns>
        [HttpGet("interface")]
        [ProducesResponseType(typeof(ICollection<LibraryInterfaceItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetInterfaces()
        {
            try
            {
                var interfaceTypes = await _libraryTypeService.GetInterfaceTypes();
                return Ok(interfaceTypes.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion InterfaceType


        #region SimpleType

        /// <summary>
        /// Get all simple types
        /// </summary>
        /// <returns></returns>
        [HttpGet("simpleType")]
        [ProducesResponseType(typeof(IEnumerable<SimpleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
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

        /// <summary>
        /// Create a simple type
        /// </summary>
        /// <returns></returns>
        [HttpPost("simpleType")]
        [ProducesResponseType(typeof(IEnumerable<LibraryType>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateSimpleType(SimpleTypeAm simpleType)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdType = await _libraryTypeService.CreateSimpleType(simpleType);
                return StatusCode(201, createdType);
            }
            catch (MimirorgDuplicateException e)
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

        #endregion SimpleType
    }
}