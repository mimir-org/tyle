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
    [SwaggerTag("Mimirorg company services")]
    public class MimirorgCompanyController : ControllerBase
    {
        private readonly IMimirorgAuthService _authService;
        private readonly IMimirorgCompanyService _companyService;
        private readonly IMimirorgUserService _userService;
        private readonly ILogger<MimirorgCompanyController> _logger;

        /// <summary>
        /// MimirorgCompanyController constructor
        /// </summary>
        /// <param name="companyService"></param>
        /// <param name="logger"></param>
        public MimirorgCompanyController(IMimirorgAuthService authService, IMimirorgCompanyService companyService, IMimirorgUserService userService, ILogger<MimirorgCompanyController> logger)
        {
            _authService = authService;
            _companyService = companyService;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get all registered companies for current user
        /// </summary>
        /// <returns>ICollection&lt;MimirorgCompanyCm&gt;</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ICollection<MimirorgCompanyCm>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Get all registered companies")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var data = await _companyService.GetAllCompanies();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to get all companies. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get a specific company by id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>MimirorgCompanyCm</returns>
        [MimirorgAuthorize(MimirorgPermission.Read, "id")]
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(MimirorgCompanyCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Get a specific company by id")]
        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            try
            {
                var data = await _companyService.GetCompanyById(id);
                return Ok(data);
            }
            catch (MimirorgNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to get company by id. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get a specific company by auth param
        /// </summary>
        /// <param name="mimirorgCompanyAuth">MimirorgCompanyAuthAm</param>
        /// <returns>MimirorgCompanyCm</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        [ProducesResponseType(typeof(MimirorgCompanyCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("Get a specific company by id")]
        public async Task<IActionResult> GetCompany([FromBody] MimirorgCompanyAuthAm mimirorgCompanyAuth)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = await _companyService.GetCompanyByAuth(mimirorgCompanyAuth);
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
                _logger.LogError(e, $"An error occurred while trying to get company by id. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Register a new company
        /// </summary>
        /// <param name="company">MimirorgCompanyAm</param>
        /// <returns>MimirorgCompanyCm</returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(MimirorgCompanyCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Register a new company")]
        public async Task<IActionResult> CreateCompany([FromBody] MimirorgCompanyAm company)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = await _companyService.CreateCompany(company);

                var manageCompanyPermission = new MimirorgUserPermissionAm
                {
                    CompanyId = data.Id,
                    UserId = data.Manager.Id,
                    Permission = MimirorgPermission.Manage
                };
                await _authService.SetPermission(manageCompanyPermission);

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
                _logger.LogError(e, $"An error occurred while trying to register a new company. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to register a new company. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Delete a registered company
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>MimirorgCompanyCm</returns>
        [MimirorgAuthorize(MimirorgPermission.Manage, "id")]
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Delete a registered company")]
        public async Task<IActionResult> DeleteCompany([FromRoute] int id)
        {
            try
            {
                var data = await _companyService.DeleteCompany(id);
                return Ok(data);
            }
            catch (MimirorgNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to delete tje company with id {id}. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Update a registered company
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="company">MimirorgCompanyAm</param>
        /// <returns>MimirorgCompanyCm</returns>
        [MimirorgAuthorize(MimirorgPermission.Manage, "id")]
        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(MimirorgCompanyCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Update a registered company")]
        public async Task<IActionResult> UpdateCompany([FromRoute] int id, [FromBody] MimirorgCompanyAm company)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = await _companyService.UpdateCompany(id, company);
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
            catch (MimirorgNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to update a company. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get the users for a company
        /// </summary>
        /// <returns>ICollection&lt;MimirorgCompanyCm&gt;</returns>
        [MimirorgAuthorize(MimirorgPermission.Manage, "id")]
        [HttpGet]
        [Route("{id:int}/users")]
        [ProducesResponseType(typeof(ICollection<MimirorgUserCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation("Get users that belong to a company")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
        {
            try
            {
                var users = await _companyService.GetCompanyUsers(id);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to get users. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get the users that have access claims connected to a company
        /// </summary>
        /// <returns>ICollection&lt;MimirorgCompanyCm&gt;</returns>
        [MimirorgAuthorize(MimirorgPermission.Manage, "id")]
        [HttpGet]
        [Route("{id:int}/authusers")]
        [ProducesResponseType(typeof(ICollection<MimirorgUserCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation("Get users that have access claims connected to a company")]
        public async Task<IActionResult> GetAuthorizedUsers([FromRoute] int id)
        {
            try
            {
                var users = await _companyService.GetAuthorizedCompanyUsers(id);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to get users. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get all pending users that the requesting entity can manage
        /// </summary>
        /// <returns>A list of users</returns>
        [Authorize]
        [HttpGet]
        [Route("users/pending")]
        [ProducesResponseType(typeof(ICollection<MimirorgUserCm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation("Get all pending users that the requesting entity can manage")]
        public async Task<IActionResult> GetPendingUsers()
        {
            try
            {
                var userCompanyIds = await _userService.GetCompaniesForUser(User, MimirorgPermission.Manage);
                var users = await _companyService.GetCompanyPendingUsers(userCompanyIds);

                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to get pending users. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Register a hook for cache invalidation
        /// </summary>
        /// <param name="hook">The hook that should be registered</param>
        /// <returns>The created hook</returns>
        [MimirorgAuthorize(MimirorgPermission.Manage, "hook", "CompanyId")]
        [HttpPost]
        [Route("hook")]
        [ProducesResponseType(typeof(MimirorgHookCm), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("Update a registered company")]
        public async Task<IActionResult> CreateHook([FromBody] MimirorgHookAm hook)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = await _companyService.CreateHook(hook);
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
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while trying to create a new hook. Error: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}