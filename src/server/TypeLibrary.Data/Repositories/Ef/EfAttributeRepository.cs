using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeLibDm>, IEfAttributeRepository
{
    public EfAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var attribute = await GetAsync(id);
        attribute.State = state;
        await SaveAsync();
        Detach(attribute);
    }

    /// <inheritdoc />
    public async Task<int> ChangeState(State state, ICollection<string> ids)
    {
        var attributesToChange = new List<AttributeLibDm>();
        foreach (var id in ids)
        {
            var attribute = await GetAsync(id);
            attribute.State = state;
            attributesToChange.Add(attribute);
        }

        await SaveAsync();
        Detach(attributesToChange);

        return attributesToChange.Count;
    }

    /// <inheritdoc />
    public IEnumerable<AttributeLibDm> Get()
    {
        return GetAll()
            .Include(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .AsSplitQuery();
    }

    /// <inheritdoc />
    public AttributeLibDm Get(string id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<AttributeLibDm> Create(AttributeLibDm attribute)
    {
        await CreateAsync(attribute);
        await SaveAsync();

        Detach(attribute);

        return attribute;
    }

    /// <inheritdoc />
    public async Task<List<AttributeLibDm>> Create(List<AttributeLibDm> attributes)
    {
        foreach (var attribute in attributes)
            await CreateAsync(attribute);
        await SaveAsync();

        Detach(attributes);

        return attributes;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}