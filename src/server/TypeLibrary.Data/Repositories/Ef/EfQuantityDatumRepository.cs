using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfQuantityDatumRepository : GenericRepository<TypeLibraryDbContext, QuantityDatumLibDm>, IEfQuantityDatumRepository
{
    public EfQuantityDatumRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var quantityDatum = await GetAsync(id);
        quantityDatum.State = state;
        await SaveAsync();
        Detach(quantityDatum);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> Get()
    {
        return GetAll();
    }

    /// <inheritdoc />
    public QuantityDatumLibDm Get(string id)
    {
        return FindBy(x => x.Id == id).FirstOrDefault();
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumRangeSpecifying()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumRangeSpecifying);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumSpecifiedScope()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumSpecifiedScope);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumSpecifiedProvenance()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumSpecifiedProvenance);
    }

    /// <inheritdoc />
    public IEnumerable<QuantityDatumLibDm> GetQuantityDatumRegularitySpecified()
    {
        return GetAll().Where(x => x.QuantityDatumType == QuantityDatumType.QuantityDatumRegularitySpecified);
    }

    /// <inheritdoc />
    public async Task<QuantityDatumLibDm> Create(QuantityDatumLibDm quantityDatum)
    {
        await CreateAsync(quantityDatum);
        await SaveAsync();

        Detach(quantityDatum);

        return quantityDatum;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}