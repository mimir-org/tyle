using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;

namespace Mimirorg.Authentication.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion(VersionConstant.OnePointZero)]
[Route("V{version:apiVersion}/[controller]")]
[SwaggerTag("Mimirorg authorize services")]
public class MimirorgAuthorizeController : ControllerBase
{
    private readonly ILogger<MimirorgAuthorizeController> _logger;
    private readonly IMimirorgAuthService _authService;

    public MimirorgAuthorizeController(ILogger<MimirorgAuthorizeController> logger, IMimirorgAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    #region Roles

    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>ICollection&lt;MimirorgRoleCm&gt;</returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("role")]
    [ProducesResponseType(typeof(ICollection<MimirorgRoleCm>), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Get all roles")]
    public async Task<IActionResult> GetRoles()
    {
        try
        {
            var data = await _authService.GetAllRoles();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to get all roles. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Add user to role
    /// </summary>
    /// <param name="userRole">MimirorgUserRoleAm</param>
    /// <returns>bool</returns>
    [MimirorgAuthorize(MimirorgPermission.Manage)]
    [HttpPost]
    [Route("role/add")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Add user to role")]
    public async Task<IActionResult> AddUserToRole(MimirorgUserRoleAm userRole)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _authService.AddUserToRole(userRole);
            return Ok(data);
        }
        catch (MimirorgNotFoundException)
        {
            return NotFound();
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
            _logger.LogError(e, $"An error occurred while trying to add user to role. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Remove user from a role
    /// </summary>
    /// <param name="userRole">MimirorgUserRoleAm</param>
    /// <returns>bool</returns>
    [MimirorgAuthorize(MimirorgPermission.Manage)]
    [HttpPost]
    [Route("role/remove")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Remove user from a role")]
    public async Task<IActionResult> RemoveUserFromRole(MimirorgUserRoleAm userRole)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _authService.RemoveUserFromRole(userRole);
            return Ok(data);
        }
        catch (MimirorgNotFoundException)
        {
            return NotFound();
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
            _logger.LogError(e, $"An error occurred while trying to remove user from role. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }


    #endregion

    #region Permissions

    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns>ICollection&lt;MimirorgRoleCm&gt;</returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("permission")]
    [ProducesResponseType(typeof(ICollection<MimirorgRoleCm>), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Get all permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        try
        {
            var data = await _authService.GetAllPermissions();
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to get all roles. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Set user permission for given company
    /// </summary>
    /// <returns>No content</returns>
    /// <remarks>Authenticate</remarks>
    [MimirorgAuthorize(MimirorgPermission.Manage, "userPermission", "CompanyId")]
    [HttpPost]
    [Route("permission/add")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Set user permissions for given company and user")]
    public async Task<IActionResult> SetUserPermission(MimirorgUserPermissionAm userPermission)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _authService.SetPermission(userPermission);
            return NoContent();
        }
        catch (MimirorgNotFoundException)
        {
            return NotFound();
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
            _logger.LogError(e, $"An error occurred while trying to get all roles. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Remove user permission for given company
    /// </summary>
    /// <returns>No content</returns>
    /// <remarks>Authenticate</remarks>
    [MimirorgAuthorize(MimirorgPermission.Manage, "userPermission", "CompanyId")]
    [HttpPost]
    [Route("permission/remove")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Set user permissions for given company and user")]
    public async Task<IActionResult> RemoveUserPermission(MimirorgUserPermissionAm userPermission)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _authService.RemovePermission(userPermission);
            return NoContent();
        }
        catch (MimirorgNotFoundException)
        {
            return NotFound();
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
            _logger.LogError(e, $"An error occurred while trying to get all roles. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    #endregion
}