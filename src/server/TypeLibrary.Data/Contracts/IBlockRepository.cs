using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IBlockRepository
{
    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The block id</param>
    /// <returns>The company id of given block</returns>
    //int HasCompany(string id);

    /// <summary>
    /// Change the state of the block with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The block id</param>
    //Task ChangeState(State state, string id);

    /// <summary>
    /// Get all blocks
    /// </summary>
    /// <returns>A collection of blocks</returns>
    IEnumerable<BlockType> Get();

    /// <summary>
    /// Get block by id
    /// </summary>
    /// <param name="id">The block id</param>
    /// <returns>Block if found</returns>
    BlockType Get(Guid id);

    /// <summary>
    /// Get all versions of the given block
    /// </summary>
    /// <param name="block">The block</param>
    /// <returns>A collection of all versions of the block</returns>
    //IEnumerable<BlockType> GetAllVersions(BlockType block);

    /// <summary>
    /// Create a block
    /// </summary>
    /// <param name="block">The block to be created</param>
    /// <returns>The created block</returns>
    Task<BlockType> Create(BlockType block);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}