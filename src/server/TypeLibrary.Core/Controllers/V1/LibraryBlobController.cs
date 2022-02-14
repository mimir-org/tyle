using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [SwaggerTag("Blob (icons) services")]
    public class LibraryBlobController : ControllerBase
    {
        private readonly ILogger<LibraryBlobController> _logger;
        private readonly IBlobService _blobService;

        public LibraryBlobController(ILogger<LibraryBlobController> logger, IBlobService blobService)
        {
            _logger = logger;
            _blobService = blobService;
        }

        /// <summary>
        /// Get blob data from category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BlobLibAm>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Read")]
        public IActionResult GetBlobData()
        {
            try
            {
                var blobs = _blobService.GetBlob().ToList();
                return Ok(blobs);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Create a new blob data object
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BlobLibDm), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> CreateOrUpdateBlob([FromBody] BlobLibAm blob)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdBlob = await _blobService.CreateBlob(blob);
                return StatusCode(201, createdBlob);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}