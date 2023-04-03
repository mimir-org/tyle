using System.Collections.Generic;
using System.Linq;
using Mimirorg.Authentication.Contracts;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Core.Factories;

public class CompanyFactory : ICompanyFactory
{
    private readonly ICacheRepository _companyCache;
    private readonly IMimirorgCompanyService _mimirorgCompanyService;

    public CompanyFactory(ICacheRepository companyCache, IMimirorgCompanyService mimirorgCompanyService)
    {
        _companyCache = companyCache;
        _mimirorgCompanyService = mimirorgCompanyService;
    }

    public string GetCompanyName(int? companyId)
    {
        if (companyId == null) return null;

        var companyName = _companyCache.GetOrCreateAsync($"company-{companyId}", async () => await _mimirorgCompanyService.GetCompanyById(companyId.Value)).Result.Name;

        return companyName;
    }
}