using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Attributes;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Swashbuckle.AspNetCore.Annotations;

namespace Mimirorg.Authentication.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
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
        [ProducesResponseType(typeof(MimirorgUserCm), StatusCodes.Status200OK)]
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
        /// Get pending users
        /// </summary>
        /// <returns>Pending users</returns>
        [MimirorgAuthorize(MimirorgPermission.Manage, "companyId")]
        [HttpGet]
        [Route("pending/{companyId}")]
        [ProducesResponseType(typeof(ICollection<MimirorgUserCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation("Get current authenticated user")]
        public IActionResult GetPendingUsers([FromRoute] int companyId)
        {
            try
            {
                var users = _userService.GetPendingUsers(companyId).ToList();
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to get pending users. Error: {e.Message}");
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
        [ProducesResponseType(typeof(MimirorgUserCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("Create a new user")]
        public async Task<IActionResult> CreateUser([FromBody] MimirorgUserAm user)
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
        public async Task<IActionResult> VerifyAccount([FromBody] MimirorgVerifyAm verifyEmail)
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
                _logger.LogError(e, $"An error occurred while trying to verify account. The operation is invalid Error: {e.Message}");
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
        [ProducesResponseType(typeof(MimirorgQrCodeCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("VerifyTwoFactor")]
        public async Task<IActionResult> VerifyTwoFactor([FromBody] MimirorgVerifyAm data)
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
                _logger.LogError(e, $"An error occurred while trying to verify account. The operation is invalid Error: {e.Message}");
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
        public async Task<IActionResult> ChangePassword([FromBody] MimirorgChangePasswordAm changePassword)
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
                _logger.LogError(e, $"An error occurred while trying to verify account. The operation is invalid Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to verify account. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}