using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeLibDm>, IEfAttributeRepository
{
    private readonly IApplicationSettingsRepository _settings;
    private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

    public EfAttributeRepository(IApplicationSettingsRepository settings, TypeLibraryDbContext dbContext, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
    {
        _settings = settings;
        _typeLibraryProcRepository = typeLibraryProcRepository;
    }

    public async Task<int> HasCompany(int id)
    {
        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Attribute"},
            {"@Id", id}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
        return result?.FirstOrDefault()?.CompanyId ?? 0;
    }

    public async Task<int> ChangeState(State state, ICollection<int> ids)
    {
        if (ids == null)
            return 0;

        var idList = string.Join(",", ids.Select(i => i.ToString()));

        var procParams = new Dictionary<string, object>
        {
            {"@TableName", "Attribute"},
            {"@State", state.ToString()},
            {"@IdList", idList}
        };

        var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
        return result?.FirstOrDefault()?.Number ?? 0;
    }

    public IEnumerable<AttributeLibDm> Get()
    {
        return GetAll()
            .Include(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .AsSplitQuery();
    }

    public AttributeLibDm Get(int id)
    {
        return FindBy(x => x.Id == id)
            .Include(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .AsSplitQuery()
            .FirstOrDefault();
    }

    /// <summary>
    /// Create an attribute
    /// </summary>
    /// <param name="attribute">The attribute to be created</param>
    /// <returns>The created attribute</returns>
    public async Task<AttributeLibDm> Create(AttributeLibDm attribute)
    {
        await CreateAsync(attribute);
        await SaveAsync();

        attribute.Iri = $"{_settings.ApplicationSemanticUrl}/attribute/{attribute.Id}";
        foreach (var attributeUnit in attribute.AttributeUnits)
            attributeUnit.AttributeId = attribute.Id;
        await SaveAsync();

        Detach(attribute);

        return attribute;
    }

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }
}