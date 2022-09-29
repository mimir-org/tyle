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
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// Library services
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
        private readonly IMimirorgAuthService _authService;

        public LibraryNodeController(ILogger<LibraryNodeController> logger, INodeService nodeService, IMimirorgAuthService authService)
        {
            _logger = logger;
            _nodeService = nodeService;
            _authService = authService;
        }

        /// <summary>
        /// Get all nodes
        /// </summary>
        /// <returns>A collection of nodes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<NodeLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult GetLatestVersions()
        {
            try
            {
                var cm = _nodeService.GetLatestVersions().ToList();
                return Ok(cm);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get node by id
        /// </summary>
        /// <param name="id">node id</param>
        /// <returns>The content if exist or </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult GetLatestVersion([FromRoute] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = _nodeService.GetLatestVersion(id);

                if (data == null)
                    return NotFound(id);

                return Ok(data);
            }
            catch (MimirorgNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Create a node
        /// </summary>
        /// <param name="node">The node that should be created</param>
        /// <returns>The created node</returns>
        [HttpPost]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "node", "CompanyId")]
        public async Task<IActionResult> Create([FromBody] NodeLibAm node)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cm = await _nodeService.Create(node);
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

        /// <summary>
        /// Update node
        /// </summary>
        /// <param name="nodeAm"></param>
        /// <returns>NodeLibCm</returns>
        [HttpPut]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [MimirorgAuthorize(MimirorgPermission.Write, "node", "CompanyId")]
        public async Task<IActionResult> Update([FromBody] NodeLibAm nodeAm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var companyId = await _nodeService.GetCompanyId(nodeAm.Id);

                if (companyId != nodeAm.CompanyId)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _nodeService.Update(nodeAm);
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
        /// Update node with new state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="id"></param>
        /// <returns>NodeLibCm</returns>
        [HttpPatch("state/{id}")]
        [ProducesResponseType(typeof(NodeLibCm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> ChangeState([FromBody] State state, [FromRoute] string id)
        {
            try
            {
                var companyId = await _nodeService.GetCompanyId(id);
                var hasAccess = await _authService.HasAccess(companyId, state);

                if (!hasAccess)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var data = await _nodeService.ChangeState(id, state);
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