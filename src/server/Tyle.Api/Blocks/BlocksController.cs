using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Constants;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Api.Common;
using Tyle.Application.Blocks;
using Tyle.Application.Blocks.Requests;
using Tyle.Application.Common;
using Tyle.Core.Common;

namespace Tyle.Api.Blocks;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Authorize]
[Route("[controller]")]
[SwaggerTag("Block services")]
public class BlocksController : ControllerBase
{
    private readonly IMimirorgAuthService _authService;
    private readonly IBlockRepository _blockRepository;
    private readonly IMapper _mapper;
    private readonly IApprovalService _approvalService;

    public BlocksController(IBlockRepository blockRepository, IMapper mapper, IApprovalService approvalService, IMimirorgAuthService authService)
    {
        _authService = authService;
        _blockRepository = blockRepository;
        _mapper = mapper;
        _approvalService = approvalService;
    }

    /// <summary>
    /// Get all blocks
    /// </summary>
    /// <returns>A collection of blocks</returns>
    [HttpGet]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(ICollection<BlockView>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(State? state = null)
    {
        try
        {
            var blocks = await _blockRepository.GetAll(state);
            return Ok(_mapper.Map<IEnumerable<BlockView>>(blocks));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get block by id
    /// </summary>
    /// <param name="id">The id of the block to get</param>
    /// <returns>The requested block</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}, {MimirorgDefaultRoles.Reader}")]
    [ProducesResponseType(typeof(BlockView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var block = await _blockRepository.Get(id);

            if (block == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BlockView>(block));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Create a block
    /// </summary>
    /// <param name="request">The block that should be created</param>
    /// <returns>The created block</returns>
    [HttpPost]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}")]
    [ProducesResponseType(typeof(BlockView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] BlockTypeRequest request)
    {
        try
        {
            var createdBlock = await _blockRepository.Create(request);
            return Created("dummy", _mapper.Map<BlockView>(createdBlock));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update a block
    /// </summary>
    /// <param name="id">The id of the block that should be updated</param>
    /// <param name="request">The new values of the block</param>
    /// <returns>The updated block</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = $"{MimirorgDefaultRoles.Administrator}, {MimirorgDefaultRoles.Reviewer}, {MimirorgDefaultRoles.Contributor}")]
    [ProducesResponseType(typeof(BlockView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BlockTypeRequest request)
    {
        try
        {
            var block = await _blockRepository.Update(id, request);

            if (block == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BlockView>(block));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Change the state of a block.
    /// </summary>
    /// <param name="id">The id of the block that will change state.</param>
    /// <param name="request">A request containing the wanted state.</param>
    [HttpPatch("{id}/state")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeState([FromRoute] Guid id, [FromBody] StateChangeRequest request)
    {
        try
        {
            if (!_authService.HasUserPermissionToUpdateToState(User, request.State))
            {
                return StatusCode(403);
            }

            var response = await _approvalService.ChangeBlockState(id, request.State);

            return response switch
            {
                ApprovalResponse.Accepted => NoContent(),
                ApprovalResponse.TypeNotFound => NotFound(),
                ApprovalResponse.IllegalChange => BadRequest(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete a block that is not approved
    /// </summary>
    /// <param name="id">The id of the block to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            var blockFromDb = await _blockRepository.Get(id);
            if (blockFromDb == null)
                return NotFound();

            if (!_authService.HasUserPermissionToDelete(User, blockFromDb))
            {
                return StatusCode(403);
            }

            if (await _blockRepository.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}