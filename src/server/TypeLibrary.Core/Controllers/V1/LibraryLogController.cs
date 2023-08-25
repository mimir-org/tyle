/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Core.Controllers.V1;

/// <summary>
/// Library services
/// </summary>
[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Library Log Services")]
public class LibraryLogController : ControllerBase
{
    private readonly ILogger<LibraryLogController> _logger;
    private readonly ILogService _logService;
    private readonly IApprovalService _approvalService;

    public LibraryLogController(ILogger<LibraryLogController> logger, ILogService logService, IApprovalService approvalService)
    {
        _logger = logger;
        _logService = logService;
        _approvalService = approvalService;
    }

    /// <summary>
    /// Get all logs
    /// </summary>
    /// <returns>A collection of all logs</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<LogLibCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Manage)]
    public IActionResult Get()
    {
        try
        {
            var cm = _logService.Get().ToList();
            return Ok(cm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get all approvals
    /// </summary>
    /// <returns>A collection of all approvals</returns>
    [HttpGet("approvals")]
    [ProducesResponseType(typeof(ICollection<ApprovalCm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetApprovals()
    {
        try
        {
            var approvals = await _approvalService.GetApprovals();
            return Ok(approvals);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Internal Server Error {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

}*/