using System;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TypeLibrary.Services.Contracts;

public interface IBlockService
{
    /// <summary>
    /// Get the latest approved block versions as well as any unfinished or in review drafts
    /// </summary>
    /// <returns>A collection of blocks</returns>
    IEnumerable<BlockLibCm> GetLatestVersions();

    /// <summary>
    /// Get a block based on given id
    /// </summary>
    /// <param name="id">The id of the block</param>
    /// <returns>The block of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no block with the given id.</exception>
    BlockLibCm Get(Guid id);

    /*/// <summary>
    /// Get the latest approved version of a block
    /// </summary>
    /// <param name="id">The id of the block we want to get the latest approved version of</param>
    /// <returns>The latest approved version of the block of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no latest approved block with the given id.</exception>
    BlockLibCm GetLatestApproved(Guid id);*/

    /// <summary>
    /// Create a new block
    /// </summary>
    /// <param name="blockAm">The block that should be created</param>
    /// <returns>The created block</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if block is not valid</exception>
    /// <remarks>Remember that creating a new block could be creating a new version of existing block.
    /// They will have the same first version id, but have different version and id.</remarks>
    Task<BlockLibCm> Create(BlockLibAm blockAm);

    /*/// <summary>
    /// Update a block if the data is allowed to be changed.
    /// </summary>
    /// <param name="id">The id of the block to update</param>
    /// <param name="blockAm">The new block values</param>
    /// <returns>The updated block</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the block with the given id is not found.</exception>
    /// <exception cref="MimirorgBadRequestException">Throws if the block is not valid.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the block is a state that makes it invalid for updates,
    /// a draft already exists for this type or if changes are not allowed.</exception>
    Task<BlockLibCm> Update(string id, BlockLibAm blockAm);*/

    /// <summary>
    ///  Delete a block, it can't be approved
    /// </summary>
    /// <param name="id">The id of the block to delete</param>
    /// <exception cref="MimirorgNotFoundException">Throws if the block with the given id is not found.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the block in question can't be deleted.</exception>
    Task Delete(Guid id);

    /*/// <summary>
    /// Change block state
    /// </summary>
    /// <param name="id">The id of the block that should change state</param>
    /// <param name="state">The new block state</param>
    /// <param name="sendStateEmail"></param>
    /// <returns>An approval data object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the block does not exist</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the block is already
    /// approved, is identical to an already approved block or contains references to deleted or unapproved
    /// terminals, attributes or RDS.</exception>
    Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail);

    /// <summary>
    /// Get the company id of a block
    /// </summary>
    /// <param name="id">The block id</param>
    /// <returns>Company id for the block</returns>
    int GetCompanyId(string id);*/
}