using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeType>, IEfAttributeRepository
{
    public EfAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /*/// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var attribute = await GetAsync(id);
        attribute.State = state;
        await SaveAsync();
        Detach(attribute);
    }*/

    /// <inheritdoc />
    public IEnumerable<AttributeType> Get()
    {
        return GetAll()
            .Include(x => x.ValueConstraint)
            .Include(x => x.AttributeGroups)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public AttributeType Get(Guid id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.ValueConstraint)
            .Include(x => x.AttributeGroups)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<AttributeType> Create(AttributeType attribute)
    {
        await CreateAsync(attribute);
        await SaveAsync();

        Detach(attribute);

        return attribute;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}