using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfUnitRepository : GenericRepository<TypeLibraryDbContext, UnitLibDm>, IEfUnitRepository
{
    public EfUnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
    public int HasCompany(string id)
    {
        return Get(id).CompanyId ?? 0;
    }

    /// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var unit = await GetAsync(id);
        unit.State = state;
        await SaveAsync();
        Detach(unit);
    }

    /// <inheritdoc />
    public async Task<int> ChangeState(State state, ICollection<string> ids)
    {
        var unitsToChange = new List<UnitLibDm>();
        foreach (var id in ids)
        {
            var unit = await GetAsync(id);
            unit.State = state;
            unitsToChange.Add(unit);
        }

        await SaveAsync();
        Detach(unitsToChange);

        return unitsToChange.Count;
    }

    /// <inheritdoc />
    public IEnumerable<UnitLibDm> Get()
    {
        return GetAll();
    }

    /// <inheritdoc />
    public UnitLibDm Get(string id)
    {
        return FindBy(x => x.Id == id).FirstOrDefault();
    }

    /// <inheritdoc />
    public UnitLibDm GetByTypeReference(string typeReference)
    {
        return FindBy(x => x.TypeReference == typeReference).FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<UnitLibDm> Create(UnitLibDm unit)
    {
        await CreateAsync(unit);
        await SaveAsync();

        Detach(unit);

        return unit;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}