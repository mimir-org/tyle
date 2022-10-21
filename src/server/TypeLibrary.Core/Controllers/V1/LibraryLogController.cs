using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.TypeLibrary.Enums;
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
    [SwaggerTag("Library Log Services")]
    public class LibraryLogController : ControllerBase
    {
        private readonly ILogger<LibraryLogController> _logger;
        private readonly ILogService _logService;

        public LibraryLogController(ILogger<LibraryLogController> logger, ILogService logService)
        {
            _logger = logger;
            _logService = logService;
        }

        /// <summary>
        /// Get all logs
        /// </summary>
        /// <returns>A collection of all logs</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<LogLibCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[MimirorgAuthorize(MimirorgPermission.Manage)]
        public IActionResult Get()
        {
            try
            {
                var cm = _logService.Get().ToList();
                return Ok(cm);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

    }
}