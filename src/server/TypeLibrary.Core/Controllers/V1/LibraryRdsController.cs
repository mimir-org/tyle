using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Client;
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
        /// Get all RDS codes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<RdsLibCm>), StatusCodes.Status200OK)]
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
    }
}