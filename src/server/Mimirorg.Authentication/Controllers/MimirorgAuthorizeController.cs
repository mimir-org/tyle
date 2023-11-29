using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace Mimirorg.Authentication.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
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

    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>ICollection&lt;MimirorgRoleCm&gt;</returns>
    [Authorize(Roles = MimirorgDefaultRoles.Administrator)]
    [HttpGet]
    [Route("role")]
    [ProducesResponseType(typeof(ICollection<RoleView>), 200)]
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
    [Authorize(Roles = MimirorgDefaultRoles.Administrator)]
    [HttpPost]
    [Route("role/add")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Add user to role")]
    public async Task<IActionResult> AddUserToRole(UserRoleRequest userRole)
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
    [Authorize(Roles = MimirorgDefaultRoles.Administrator)]
    [HttpPost]
    [Route("role/remove")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Remove user from a role")]
    public async Task<IActionResult> RemoveUserFromRole(UserRoleRequest userRole)
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
}