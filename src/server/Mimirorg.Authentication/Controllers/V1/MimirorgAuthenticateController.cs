using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Content;
using Mimirorg.Common.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using Mimirorg.Authentication.Models.Enums;

namespace Mimirorg.Authentication.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V{version:apiVersion}/[controller]")]
    [SwaggerTag("Mimirorg authenticate services")]
    public class MimirorgAuthenticateController : ControllerBase
    {
        private const string RefreshTokenCookie = "tyleToken";
        private readonly ILogger<MimirorgAuthenticateController> _logger;
        private readonly IMimirorgAuthService _authService;

        public MimirorgAuthenticateController(ILogger<MimirorgAuthenticateController> logger, IMimirorgAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Authenticate an user
        /// </summary>
        /// <param name="authenticate">MimirorgAuthenticateAm</param>
        /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(MimirorgTokenCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Login with username and password")]
        public async Task<IActionResult> Login([FromBody] MimirorgAuthenticateAm authenticate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = await _authService.Authenticate(authenticate);
                var accessToken = data.FirstOrDefault(x => x.TokenType == MimirorgTokenType.AccessToken);
                var refreshToken = data.FirstOrDefault(x => x.TokenType == MimirorgTokenType.RefreshToken);

                await AddRefreshTokenCookie(HttpContext.Request, HttpContext.Response, refreshToken);

                return Ok(accessToken);
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
        /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("secret")]
        [ProducesResponseType(typeof(MimirorgTokenCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Login with secret")]
        public async Task<IActionResult> LoginSecret()
        {
            try
            {
                if (!HttpContext.Request.Cookies.TryGetValue(RefreshTokenCookie, out var token))
                    throw new AuthenticationException("Refresh token is missing");

                var data = await _authService.Authenticate(token);
                var accessToken = data.FirstOrDefault(x => x.TokenType == MimirorgTokenType.AccessToken);
                var refreshToken = data.FirstOrDefault(x => x.TokenType == MimirorgTokenType.RefreshToken);

                await AddRefreshTokenCookie(HttpContext.Request, HttpContext.Response, refreshToken);
                return Ok(accessToken);
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
        /// Verify account from token
        /// </summary>
        /// <param name="token">string</param>
        /// <returns>bool</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("verify/{token}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Activate account")]
        public async Task<IActionResult> VerifyAccount([FromRoute] string token)
        {
            try
            {
                var data = await _authService.VerifyAccount(token);
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

        #region Private methods

        private Task AddRefreshTokenCookie(HttpRequest request, HttpResponse response, MimirorgTokenCm token)
        {
            if (response == null || token == null || request == null)
                return Task.CompletedTask;

            if (token.TokenType != MimirorgTokenType.RefreshToken)
                return Task.CompletedTask;

            if (request.Cookies.ContainsKey(RefreshTokenCookie))
                response.Cookies.Delete(RefreshTokenCookie);

            if (!string.IsNullOrWhiteSpace(token?.Secret))
                Response.Cookies.Append(RefreshTokenCookie, token.Secret, new CookieOptions
                {
                    Expires = token.ValidTo,
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                });

            return Task.CompletedTask;
        }

        #endregion
    }
}