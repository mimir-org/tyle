using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgCompanyService
{
    /// <summary>
    /// Create a company from model
    /// </summary>
    /// <param name="company">MimirorgCompanyAm</param>
    /// <returns>MimirorgCompanyCm</returns>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgInvalidOperationException"></exception>
    Task<MimirorgCompanyCm> CreateCompany(MimirorgCompanyAm company);

    /// <summary>
    /// Get a collection of all registered companies.
    /// </summary>
    /// <returns>ICollection&lt;MimirorgCompanyCm&gt;</returns>
    Task<ICollection<MimirorgCompanyCm>> GetAllCompanies();

    /// <summary>
    /// Get a company by id
    /// </summary>
    /// <param name="id">Unique identifier of a company</param>
    /// <returns>MimirorgCompanyCm</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<MimirorgCompanyCm> GetCompanyById(int id);

    /// <summary>
    /// Get a company by domain and secret
    /// </summary>
    /// <param name="mimirorgCompanyAuth">Domain and secret</param>
    /// <returns>MimirorgCompanyCm</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException"></exception>
    Task<MimirorgCompanyCm> GetCompanyByAuth(MimirorgCompanyAuthAm mimirorgCompanyAuth);

    /// <summary>
    /// Update a company
    /// </summary>
    /// <param name="id"></param>
    /// <param name="company"></param>
    /// <returns>MimirorgCompanyCm</returns>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<MimirorgCompanyCm> UpdateCompany(int id, MimirorgCompanyAm company);

    /// <summary>
    /// Delete a registered company
    /// </summary>
    /// <param name="id"></param>
    /// <returns>bool</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<bool> DeleteCompany(int id);

    /// <summary>
    /// Get all users that belong to a company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ICollection&lt;MimirorgCompanyCm&gt;</returns>
    Task<ICollection<MimirorgUserCm>> GetCompanyUsers(int id);

    /// <summary>
    /// Get all users with a claim to the company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ICollection&lt;MimirorgCompanyCm&gt;</returns>
    Task<ICollection<MimirorgUserCm>> GetAuthorizedCompanyUsers(int id);

    /// <summary>
    /// Gets all pending users that belongs to the given company ids.
    /// </summary>
    /// <param name="companyIds"></param>
    /// <returns>A list of users</returns>
    Task<ICollection<MimirorgUserCm>> GetCompanyPendingUsers(ICollection<int> companyIds);

    /// <summary>
    /// Get all registered hooks for given cache key
    /// </summary>
    /// <param name="key">The cache key to search for</param>
    /// <returns>A collection of hooks</returns>
    Task<ICollection<MimirorgHookCm>> GetAllHooksForCache(CacheKey key);

    /// <summary>
    /// Create a new hook
    /// </summary>
    /// <param name="hook">The hook to be created</param>
    /// <returns>The created hook</returns>
    Task<MimirorgHookCm> CreateHook(MimirorgHookAm hook);
}