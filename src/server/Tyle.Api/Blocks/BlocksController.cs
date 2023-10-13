using System;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Tyle.Api.Attributes;
using Tyle.Application.Blocks;
using Tyle.Application.Blocks.Requests;
using Tyle.Core.Common;

namespace Tyle.Api.Blocks;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("[controller]")]
[SwaggerTag("Block services")]
public class BlocksController : ControllerBase
{
    private readonly IBlockRepository _blockRepository;
    private readonly IMapper _mapper;

    public BlocksController(IBlockRepository blockRepository, IMapper mapper)
    {
        _blockRepository = blockRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all blocks
    /// </summary>
    /// <returns>A collection of blocks</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<BlockView>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var blocks = await _blockRepository.GetAll();
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
    [ProducesResponseType(typeof(BlockView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
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
    [ProducesResponseType(typeof(BlockView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
    public async Task<IActionResult> Create([FromBody] BlockTypeRequest request)
    {
        try
        {
            var createdBlock = await _blockRepository.Create(request);

            return Created("dummy", _mapper.Map<BlockView>(createdBlock));
        }
        catch (KeyNotFoundException ex)
        {
            return StatusCode(422, ex.Message);
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
    [ProducesResponseType(typeof(BlockView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MimirorgAuthorize(MimirorgPermission.Write, "request", "CompanyId")]
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
        catch (KeyNotFoundException ex)
        {
            return StatusCode(422, ex.Message);
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
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
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