using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.TypeLibrary.Models.Application;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// TypeDm services
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Library Node Services")]
    public class LibraryNodeController : ControllerBase
    {
        private readonly ILogger<LibraryNodeController> _logger;
        private readonly INodeService _nodeService;

        public LibraryNodeController(ILogger<LibraryNodeController> logger, INodeService nodeService)
        {
            _logger = logger;
            _nodeService = nodeService;
        }

        /// <summary>
        /// Get node by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NodeLibCm</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNode([FromRoute] string id)
        {
            try
            {
                var data = await _nodeService.GetNode(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get all nodes
        /// </summary>
        /// <returns>NodeLibCm Collection</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<NodeLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNodes()
        {
            try
            {
                var data = await _nodeService.GetNodes();
                return Ok(data.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update node
        /// </summary>
        /// <param name="dataAm"></param>
        /// <param name="id"></param>
        /// <returns>NodeLibCm</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateNode([FromBody] NodeLibAm dataAm, [FromRoute] string id)
        {
            try
            {
                var data = await _nodeService.UpdateNode(dataAm, id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Create node
        /// </summary>
        /// <param name="dataAm"></param>
        /// <returns>NodeLibCm</returns>
        [HttpPost]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateNode([FromBody] NodeLibAm dataAm)
        {
            try
            {
                var data = await _nodeService.CreateNode(dataAm);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Delete a node
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        [SwaggerOperation("Delete a node")]
        public async Task<IActionResult> DeleteNode([FromRoute] string id)
        {
            try
            {
                var data = await _nodeService.DeleteNode(id);
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