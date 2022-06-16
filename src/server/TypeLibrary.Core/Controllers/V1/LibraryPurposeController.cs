using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.TypeLibrary.Models.Application;
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
    [SwaggerTag("PurposeId services")]
    public class LibraryPurposeController : ControllerBase
    {
        private readonly ILogger<LibraryPurposeController> _logger;
        private readonly IPurposeService _purposeService;

        public LibraryPurposeController(ILogger<LibraryPurposeController> logger, IPurposeService purposeService)
        {
            _logger = logger;
            _purposeService = purposeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PurposeLibAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetPurposes()
        {
            try
            {
                var data = await _purposeService.Get();
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