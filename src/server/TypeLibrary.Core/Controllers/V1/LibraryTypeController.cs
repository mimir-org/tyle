using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// TypeDm services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("LibraryType services")]
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
        /// Get all types by search
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(LibraryTypeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetLibraryType(string name)
        {
            try
            {
                var typeCm = await _libraryTypeService.GetLibraryType(name);
                return Ok(typeCm);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        

        /// <summary>
        /// Get a typeDm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libraryTypeFilter"></param>
        /// <returns></returns>
        [HttpGet("{id}/{libraryTypeFilter}")]
        [ProducesResponseType(typeof(LibraryTypeLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetLibraryType([Required] string id, [Required] LibraryTypeFilter libraryTypeFilter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var data = await _libraryTypeService.ConvertToCreateLibraryType(id, libraryTypeFilter);
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
        /// Create a typeDm
        /// </summary>
        /// <param name="libraryTypeAm"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(LibraryTypeLibDm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateLibraryType([FromBody] LibraryTypeLibAm libraryTypeAm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (libraryTypeAm.Aspect == Aspect.Location)
                    libraryTypeAm.ObjectType = ObjectType.ObjectBlock;
                

                switch (libraryTypeAm.ObjectType)
                {
                    case ObjectType.ObjectBlock:
                        var ob = await _libraryTypeService.CreateLibraryType<NodeLibCm>(libraryTypeAm);
                        return Ok(ob);

                    case ObjectType.Transport:
                        var ln = await _libraryTypeService.CreateLibraryType<TransportLibCm>(libraryTypeAm);
                        return Ok(ln);

                    case ObjectType.Interface:
                        var libraryInterfaceItem = await _libraryTypeService.CreateLibraryType<InterfaceLibCm>(libraryTypeAm);
                        return Ok(libraryInterfaceItem);

                    default:
                        throw new MimirorgInvalidOperationException($"Can't create typeAm of: {libraryTypeAm.ObjectType}");
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
        /// Update a typeDm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libraryTypeAm"></param>
        /// <param name="updateMajorVersion"></param>
        /// <param name="updateMinorVersion"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(LibraryTypeLibDm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateLibraryType(string id, [FromBody] LibraryTypeLibAm libraryTypeAm,
            bool updateMajorVersion = false, bool updateMinorVersion = false)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                switch (libraryTypeAm.ObjectType)
                {
                    case ObjectType.ObjectBlock:
                        var ob = await _libraryTypeService.UpdateLibraryType<NodeLibCm>(id, libraryTypeAm, updateMajorVersion, updateMinorVersion);
                        return Ok(ob);

                    case ObjectType.Transport:
                        var ln = await _libraryTypeService.UpdateLibraryType<TransportLibCm>(id, libraryTypeAm, updateMajorVersion, updateMinorVersion);
                        return Ok(ln);

                    case ObjectType.Interface:
                        var interfaceCm = await _libraryTypeService.UpdateLibraryType<InterfaceLibCm>(id, libraryTypeAm, updateMajorVersion, updateMinorVersion);
                        return Ok(interfaceCm);

                    default:
                        throw new MimirorgInvalidOperationException($"Can't create typeAm of: {libraryTypeAm.ObjectType}");
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
        /// Delete a typeDm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteLibraryType(string id)
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

        #region Node

        /// <summary>
        /// Get all nodes
        /// </summary>
        /// <returns></returns>
        [HttpGet("node")]
        [ProducesResponseType(typeof(ICollection<NodeLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetNodes()
        {
            try
            {
                var data = await _libraryTypeService.GetNodes();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion Node

        #region Transport

        /// <summary>
        /// Get transport types
        /// </summary>
        /// <returns></returns>
        [HttpGet("transport")]
        [ProducesResponseType(typeof(ICollection<TransportLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetTransports()
        {
            try
            {
                var data = await _libraryTypeService.GetTransports();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion Transport


        #region Interface

        /// <summary>
        /// Get interface types
        /// </summary>
        /// <returns></returns>
        [HttpGet("interface")]
        [ProducesResponseType(typeof(ICollection<InterfaceLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetInterfaces()
        {
            try
            {
                var data = await _libraryTypeService.GetInterfaces();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion Interface


        #region SimpleDm

        /// <summary>
        /// Get all simple types
        /// </summary>
        /// <returns></returns>
        [HttpGet("simple")]
        [ProducesResponseType(typeof(IEnumerable<SimpleLibDm>), StatusCodes.Status200OK)]
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
        /// Create a simple typeAm
        /// </summary>
        /// <returns></returns>
        [HttpPost("simple")]
        [ProducesResponseType(typeof(IEnumerable<LibraryTypeLibDm>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateSimple(SimpleLibAm simpleAm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var simple = await _libraryTypeService.CreateSimpleType(simpleAm);
                return StatusCode(201, simple);
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

        #endregion SimpleDm
    }
}