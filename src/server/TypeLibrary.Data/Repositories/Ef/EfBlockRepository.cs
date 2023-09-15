using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfBlockRepository : GenericRepository<TypeLibraryDbContext, BlockType>, IEfBlockRepository
{
    public EfBlockRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /*/// <inheritdoc />
    public int HasCompany(string id)
    {
        return Get(id).CompanyId;
    }

    /// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var block = await GetAsync(id);
        block.State = state;
        await SaveAsync();
        Detach(block);
    }*/

    /// <inheritdoc />
    public IEnumerable<BlockType> Get()
    {
        return GetAll()
            .Include(x => x.Classifiers)
            .ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.Classifiers)
            .ThenInclude(x => x.Classifier)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.Purpose)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.Medium)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Predicate)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Units)
            .ThenInclude(x => x.Unit)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.ValueConstraint)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Predicate)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Units)
            .ThenInclude(x => x.Unit)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.ValueConstraint)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public BlockType Get(Guid id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.Classifiers)
            .ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.Classifiers)
            .ThenInclude(x => x.Classifier)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.Purpose)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.Medium)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Predicate)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Units)
            .ThenInclude(x => x.Unit)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .ThenInclude(x => x.TerminalAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.ValueConstraint)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Predicate)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Units)
            .ThenInclude(x => x.Unit)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.ValueConstraint)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /*/// <inheritdoc />
    public IEnumerable<BlockType> GetAllVersions(BlockType block)
    {
        return FindBy(x => x.FirstVersionId == block.FirstVersionId)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Rds)
            .AsSplitQuery();
    }*/

    /// <inheritdoc />
    public async Task<BlockType> Create(BlockType block)
    {
        await CreateAsync(block);
        await SaveAsync();

        Detach(block);

        return block;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}