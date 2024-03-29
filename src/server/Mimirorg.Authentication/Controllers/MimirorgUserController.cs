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
[SwaggerTag("MimirorgUser")]
public class MimirorgUserController : ControllerBase
{
    private readonly ILogger<MimirorgUserController> _logger;
    private readonly IMimirorgUserService _userService;

    public MimirorgUserController(ILogger<MimirorgUserController> logger, IMimirorgUserService userService)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Get current authenticated user
    /// </summary>
    /// <returns>User</returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation("Get current authenticated user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var currentUser = await _userService.GetUser(User);
            return Ok(currentUser);
        }
        catch (MimirorgNotFoundException)
        {
            return StatusCode(204, null);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to get current user. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>List of all users</returns>
    [Authorize(Roles = MimirorgDefaultRoles.Administrator)]
    [HttpGet]
    [Route("users")]
    [ProducesResponseType(typeof(UserView[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation("Get all user")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        catch (MimirorgNotFoundException)
        {
            return StatusCode(204, null);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to get users. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Create a new login user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>The created user</returns>
    /// <remarks>Create user</remarks>
    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(UserView), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Create a new user")]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _userService.CreateUser(user);
            return Ok(data);
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
        catch (MimirorgConfigurationException e)
        {
            _logger.LogCritical(e, $"An error occurred while trying to register the user. Error: {e.Message}");
            return StatusCode(500, "Configuration Error");
        }
        catch (MimirorgInvalidOperationException e)
        {
            _logger.LogError(e, $"An error occurred while trying to register the user. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
        catch (MimirorgDuplicateException e)
        {
            _logger.LogError(e, $"An error occurred while trying to register the user. Error: {e.Message}");
            return StatusCode(500, "An error occurred while trying to register the user.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to register the user. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPatch]
    [Route("")]
    [ProducesResponseType(typeof(UserView), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update a user")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserRequest user)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = await _userService.UpdateUser(user);

            return Ok(updatedUser);
        }
        catch (MimirorgNotFoundException e)
        {
            _logger.LogError(e, $"An error occurred while trying to update the user. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
        catch (MimirorgInvalidOperationException e)
        {
            _logger.LogError(e, $"An error occurred while trying to update the user. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to update the user. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Verify account from token
    /// </summary>
    /// <param name="verifyEmail">Verify data model</param>
    /// <returns>bool</returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("verify")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("VerifyAccount")]
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyRequest verifyEmail)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _userService.VerifyAccount(verifyEmail);
            return Ok(data);
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
        catch (MimirorgInvalidOperationException e)
        {
            _logger.LogError(e,
                $"An error occurred while trying to verify account. The operation is invalid Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to verify account. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Generate QrCode for setup 2FA
    /// </summary>
    /// <param name="data">Verify data model</param>
    /// <returns>bool</returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("2fa")]
    [ProducesResponseType(typeof(QrCodeView), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("VerifyTwoFactor")]
    public async Task<IActionResult> VerifyTwoFactor([FromBody] VerifyRequest data)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var qr = await _userService.GenerateTwoFactor(data);
            return Ok(qr);
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
        catch (MimirorgInvalidOperationException e)
        {
            _logger.LogError(e,
                $"An error occurred while trying to verify account. The operation is invalid Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to verify account. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Generate change password secret
    /// </summary>
    /// <param name="email">The user email address</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("password/secret/create")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Generate change password secret")]
    public async Task<IActionResult> GenerateChangePasswordSecret([FromBody] string email)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.GenerateChangePasswordSecret(email);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to create secret. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="changePassword">Change password data</param>
    /// <returns>bool</returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("password")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Change password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePassword)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _userService.ChangePassword(changePassword);
            return Ok(data);
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
        catch (MimirorgInvalidOperationException e)
        {
            _logger.LogError(e,
                $"An error occurred while trying to verify account. The operation is invalid Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while trying to verify account. Error: {e.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}