using System.Security.Authentication;
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
    [SwaggerTag("Authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IAuthService _authService;

        public AuthenticateController(ILogger<AuthenticateController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Authenticate an user
        /// </summary>
        /// <param name="authenticate"></param>
        /// <returns>Authentication tokens</returns>
        /// <remarks>Authenticate</remarks>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ICollection<TokenAm>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Login with username and password")]
        public async Task<IActionResult> Login([FromBody] AuthenticateAm authenticate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = await _authService.Authenticate(authenticate);
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
            catch (AuthenticationException e)
            {
                _logger.LogError(e, $"An error occurred while trying to authenticate the user. Error: {e.Message}");
                return StatusCode(401, "Authentication failed");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to authenticate the user. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Authenticate an user with secret
        /// </summary>
        /// <param name="secret"></param>
        /// <returns>Authentication tokens</returns>
        /// <remarks>Authenticate</remarks>
        [AllowAnonymous]
        [HttpPost]
        [Route("secret")]
        [ProducesResponseType(typeof(ICollection<TokenAm>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Login with secret")]
        public async Task<IActionResult> LoginSecret([FromBody] string secret)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = await _authService.Authenticate(secret);
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
            catch (AuthenticationException e)
            {
                _logger.LogError(e, $"An error occurred while trying to authenticate the user. Error: {e.Message}");
                return StatusCode(401, "Authentication failed");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to authenticate the user. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
