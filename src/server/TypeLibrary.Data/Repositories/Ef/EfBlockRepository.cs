using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfBlockRepository : GenericRepository<TypeLibraryDbContext, BlockLibDm>, IEfBlockRepository
{
    public EfBlockRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
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
    }

    /// <inheritdoc />
    public IEnumerable<BlockLibDm> Get()
    {
        return GetAll()
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Rds)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public BlockLibDm Get(string id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.BlockTerminals)
            .ThenInclude(x => x.Terminal)
            .Include(x => x.BlockAttributes)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Rds)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public IEnumerable<BlockLibDm> GetAllVersions(BlockLibDm block)
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
    }

    /// <inheritdoc />
    public async Task<BlockLibDm> Create(BlockLibDm block)
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