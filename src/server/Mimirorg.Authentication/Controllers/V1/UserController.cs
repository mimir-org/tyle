using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.ApplicationModels;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Exceptions;
using Swashbuckle.AspNetCore.Annotations;

namespace Mimirorg.Authentication.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("User")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>QrCode</returns>
        /// <remarks>Create user</remarks>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(QrCode), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("Login with username and password")]
        public async Task<IActionResult> Login([FromBody] UserAm user)
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
                _logger.LogError(e, $"An error occurred while trying to register the user. Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
            catch (MimirorgInvalidOperationException e)
            {
                _logger.LogError(e, $"An error occurred while trying to register the user. Error: {e.Message}");
                return StatusCode(500, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to register the user. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get current user
        /// </summary>
        /// <returns>User</returns>
        [Authorize]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation("Get current logged in user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var currentUser = await _userService.GetUser(User);
                return Ok(currentUser);
            }
            catch (MimirorgNotFoundException e)
            {
                _logger.LogError(e, $"An error occured while trying to get current user. Error: {e.Message}");
                return StatusCode(204, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while trying to get current user. Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
