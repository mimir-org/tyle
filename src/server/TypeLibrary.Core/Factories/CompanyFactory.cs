using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Exceptions;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Core.Factories;

public class CompanyFactory : ICompanyFactory
{
    private readonly ICacheRepository _companyCache;
    private readonly ILogger<CompanyFactory> _logger;
    private readonly IMimirorgCompanyService _mimirorgCompanyService;

    public CompanyFactory(ICacheRepository companyCache, ILogger<CompanyFactory> logger, IMimirorgCompanyService mimirorgCompanyService)
    {
        _companyCache = companyCache;
        _logger = logger;
        _mimirorgCompanyService = mimirorgCompanyService;
    }

    public string GetCompanyName(int? companyId)
    {
        if (companyId == null) return null;

        string companyName = null;

        try
        {
            companyName = _companyCache.GetOrCreateAsync($"company-{companyId}",
                async () => await _mimirorgCompanyService.GetCompanyById(companyId.Value)).Result.Name;
        }
        catch (MimirorgNotFoundException exception)
        {
            _logger.LogError($"Error when getting company name: {exception.Message}");
        }

        return companyName;
    }
}