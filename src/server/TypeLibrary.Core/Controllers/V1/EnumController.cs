using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1
{
    /// <summary>
    /// Library file services
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Enum services")]
    public class EnumController : ControllerBase
    {
        private readonly ILogger<AttributeTypeController> _logger;
        private readonly IEnumService _enumService;

        public EnumController(ILogger<AttributeTypeController> logger, IEnumService enumService)
        {
            _logger = logger;
            _enumService = enumService;
        }

        #region Condition

        [HttpGet("condition")]
        [ProducesResponseType(typeof(ICollection<ConditionAm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Read")]
        public async Task<IActionResult> GetConditions()
        {
            try
            {
                var data = await _enumService.GetConditions();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("condition")]
        [ProducesResponseType(typeof(ConditionAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Edit")]
        public async Task<IActionResult> UpdateCondition([FromBody] ConditionAm conditionAm)
        {
            try
            {
                var data = await _enumService.UpdateCondition(conditionAm);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("condition")]
        [ProducesResponseType(typeof(ConditionAm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateCondition([FromBody] ConditionAm conditionAm)
        {
            try
            {
                var data = await _enumService.CreateCondition(conditionAm);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Internal Server Error: Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion Condition




    }
}