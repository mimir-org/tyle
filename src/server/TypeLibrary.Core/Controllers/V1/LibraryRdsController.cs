using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.TypeLibrary.Enums;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// TypeCm file services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("RDS services")]
    public class LibraryRdsController : ControllerBase
    {
        private readonly ILogger<LibraryRdsController> _logger;
        private readonly IRdsService _rdsService;

        public LibraryRdsController(ILogger<LibraryRdsController> logger, IRdsService rdsService)
        {
            _logger = logger;
            _rdsService = rdsService;
        }

        /// <summary>
        /// Get RDS codes based on aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        [HttpGet("{aspectEnumEnum}")]
        [ProducesResponseType(typeof(ICollection<RdsLibDm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetRdsCodes(Aspect aspect)
        {
            try
            {
                var data = _rdsService.GetRds(aspect).ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

       /// <summary>
       /// Get all RDS codes
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<RdsLibDm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetRdsCodes()
        {
            try
            {
                var data = _rdsService.GetRds().ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("category")]
        [ProducesResponseType(typeof(ICollection<RdsCategoryLibAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetRdsCategories()
        {
            try
            {
                var data = await _rdsService.GetRdsCategories();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("category/{id}")]
        [ProducesResponseType(typeof(RdsCategoryLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateRdsCategory([FromBody] RdsCategoryLibAm dataAm, [FromRoute] string id)
        {
            try
            {
                var data = await _rdsService.UpdateRdsCategory(dataAm, id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("category")]
        [ProducesResponseType(typeof(RdsCategoryLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateRdsCategory([FromBody] RdsCategoryLibAm dataAm)
        {
            try
            {
                var data = await _rdsService.CreateRdsCategory(dataAm);
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