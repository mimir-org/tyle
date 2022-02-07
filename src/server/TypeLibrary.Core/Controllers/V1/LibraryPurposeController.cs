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
                var data = await _purposeService.GetPurposes();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(PurposeLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdatePurpose([FromBody] PurposeLibAm dataAm)
        {
            try
            {
                var data = await _purposeService.UpdatePurpose(dataAm);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PurposeLibAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreatePurpose([FromBody] PurposeLibAm dataAm)
        {
            try
            {
                var data = await _purposeService.CreatePurpose(dataAm);
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