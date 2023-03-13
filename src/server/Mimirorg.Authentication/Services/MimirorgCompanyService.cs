using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Services
{
    public class MimirorgCompanyService : IMimirorgCompanyService
    {
        private readonly IMimirorgCompanyRepository _mimirorgCompanyRepository;
        private readonly IMimirorgHookRepository _mimirorgHookRepository;
        private readonly ApplicationSettings _applicationSettings;
        private readonly UserManager<MimirorgUser> _userManager;

        public MimirorgCompanyService(IMimirorgCompanyRepository mimirorgCompanyRepository, IMimirorgHookRepository mimirorgHookRepository, IOptions<ApplicationSettings> applicationSettings, UserManager<MimirorgUser> userManager)
        {
            _mimirorgCompanyRepository = mimirorgCompanyRepository;
            _mimirorgHookRepository = mimirorgHookRepository;
            _applicationSettings = applicationSettings?.Value;
            _userManager = userManager;
        }

        /// <summary>
        /// Create a company from model
        /// </summary>
        /// <param name="company">MimirorgCompanyAm</param>
        /// <returns>MimirorgCompanyCm</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgInvalidOperationException"></exception>
        public async Task<MimirorgCompanyCm> CreateCompany(MimirorgCompanyAm company)
        {
            var validation = company.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register: {company.DisplayName ?? company.Name}", validation);

            if (_mimirorgCompanyRepository.FindBy(x => x.Name != null && x.Name.ToLower() == company.Name.ToLower()).Any())
                throw new MimirorgBadRequestException($"{nameof(company.Name)} must be unique", new Validation(nameof(company.Name), $"{nameof(company.Name)} must be unique"));

            if (_mimirorgCompanyRepository.FindBy(x => x.Domain != null && x.Domain.ToLower() == company.Domain.ToLower()).Any())
                throw new MimirorgBadRequestException($"{nameof(company.Domain)} must be unique", new Validation(nameof(company.Domain), $"{nameof(company.Domain)} must be unique"));


            var domainCompany = company.ToDomainModel();
            await _mimirorgCompanyRepository.CreateAsync(domainCompany);
            await _mimirorgCompanyRepository.Context.SaveChangesAsync();

            if (domainCompany.Id < 1)
                throw new MimirorgInvalidOperationException($"Could not create company with name {company.Name}");

            return await GetCompanyById(domainCompany.Id);
        }

        /// <summary>
        /// Get a collection of all registered companies.
        /// </summary>
        /// <returns>ICollection&lt;MimirorgCompanyCm&gt;</returns>
        public async Task<ICollection<MimirorgCompanyCm>> GetAllCompanies()
        {
            var companies = _mimirorgCompanyRepository.GetAll().Select(x => x.ToContentModel()).ToList();
            companies = companies.Select(x =>
            {
                x.Logo = $"{_applicationSettings.ApplicationUrl}/logo/{x.Id}.svg";
                return x;
            }).ToList();

            return await Task.FromResult(companies);
        }

        /// <summary>
        /// Get a company by id
        /// </summary>
        /// <param name="id">Unique identifier of a company</param>
        /// <returns>MimirorgCompanyCm</returns>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<MimirorgCompanyCm> GetCompanyById(int id)
        {
            var company = await _mimirorgCompanyRepository.FindBy(x => x.Id == id).Include(x => x.Manager).FirstOrDefaultAsync();
            if (company == null)
                throw new MimirorgNotFoundException($"Could not find company with id {id}");

            var companyCm = company.ToContentModel();
            companyCm.Logo = $"{_applicationSettings.ApplicationUrl}/logo/{companyCm.Id}.svg";
            return companyCm;
        }

        /// <summary>
        /// Get a company by domain and secret
        /// </summary>
        /// <param name="mimirorgCompanyAuth">Domain and secret</param>
        /// <returns>MimirorgCompanyCm</returns>
        /// <exception cref="MimirorgNotFoundException"></exception>
        /// <exception cref="MimirorgBadRequestException"></exception>
        public async Task<MimirorgCompanyCm> GetCompanyByAuth(MimirorgCompanyAuthAm mimirorgCompanyAuth)
        {
            var validation = mimirorgCompanyAuth.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("The model for mimirorg auth is not valid.", validation);

            var company = await _mimirorgCompanyRepository
                .FindBy(x => x.Domain == mimirorgCompanyAuth.Domain && x.Secret == mimirorgCompanyAuth.Secret)
                .Include(x => x.Manager).FirstOrDefaultAsync();

            if (company == null)
                throw new MimirorgNotFoundException($"Could not find company with auth param");

            var companyCm = company.ToContentModel();
            companyCm.Logo = $"{_applicationSettings.ApplicationUrl}/logo/{companyCm.Id}.svg";
            return companyCm;
        }

        /// <summary>
        /// Update a company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        /// <returns>MimirorgCompanyCm</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<MimirorgCompanyCm> UpdateCompany(int id, MimirorgCompanyAm company)
        {
            var validation = company.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register: {company.DisplayName ?? company.Name}", validation);

            var exist = await _mimirorgCompanyRepository.GetAll().AnyAsync(x => x.Id == id);
            if (!exist)
                throw new MimirorgNotFoundException($"Could not find company with id {id}");

            var domainCompany = company.ToDomainModel();
            domainCompany.Id = id;

            _mimirorgCompanyRepository.Update(domainCompany);
            await _mimirorgCompanyRepository.SaveAsync();
            return await GetCompanyById(domainCompany.Id);
        }

        /// <summary>
        /// Delete a registered company
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<bool> DeleteCompany(int id)
        {
            var exist = await _mimirorgCompanyRepository.GetAll().AnyAsync(x => x.Id == id);
            if (!exist)
                throw new MimirorgNotFoundException($"Could not find company with id {id}");

            await _mimirorgCompanyRepository.Delete(id);
            var status = await _mimirorgCompanyRepository.SaveAsync();
            return status == 1;
        }

        public async Task<ICollection<MimirorgUserCm>> GetCompanyUsers(int id)
        {
            var users = _userManager.Users.Where(x => x.CompanyId == id).ToList();
            if (!users.Any())
            {
                return Array.Empty<MimirorgUserCm>();
            }

            var companies = await GetAllCompanies();
            var permissions = MimirorgPermissionCm.FromPermissionEnum().ToList();
            var mappedUsers = new List<MimirorgUserCm>();

            foreach (var user in users)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                if (!claims.Any())
                {
                    continue;
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userCm = user.ToContentModel();
                userCm.ResolvePermissions(roles, claims, companies, permissions);
                userCm.ResolveRoles(roles, claims, companies, permissions);
                mappedUsers.Add(userCm);
            }

            return mappedUsers;
        }

        public async Task<ICollection<MimirorgUserCm>> GetAuthorizedCompanyUsers(int id)
        {
            var allUsers = _userManager.Users.ToList();

            var companies = await GetAllCompanies();
            var permissions = MimirorgPermissionCm.FromPermissionEnum().ToList();
            var mappedUsers = new List<MimirorgUserCm>();

            foreach (var user in allUsers)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                if (!claims.Any() && !roles.Any())
                {
                    continue;
                }

                var userCm = user.ToContentModel();
                userCm.ResolvePermissions(roles, claims, companies, permissions);
                userCm.ResolveRoles(roles, claims, companies, permissions);
                mappedUsers.Add(userCm);
            }

            return mappedUsers;
        }

        /// <summary>
        /// Gets all pending users that belongs to the given company ids.
        /// </summary>
        /// <param name="companyIds"></param>
        /// <returns>A list of users</returns>
        public async Task<ICollection<MimirorgUserCm>> GetCompanyPendingUsers(ICollection<int> companyIds)
        {
            var users = _userManager.Users.Where(x => companyIds.Any(y => y == x.CompanyId)).ToList();
            if (!users.Any())
            {
                return Array.Empty<MimirorgUserCm>();
            }

            var companies = await GetAllCompanies();
            var permissions = MimirorgPermissionCm.FromPermissionEnum().ToList();
            var mappedUsers = new List<MimirorgUserCm>();

            foreach (var user in users)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                if (claims.Any())
                {
                    continue;
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userCm = user.ToContentModel();
                userCm.ResolvePermissions(roles, claims, companies, permissions);
                userCm.ResolveRoles(roles, claims, companies, permissions);
                mappedUsers.Add(userCm);
            }

            return mappedUsers;
        }

        /// <summary>
        /// Get all registered hooks for given cache key
        /// </summary>
        /// <param name="key">The cache key to search for</param>
        /// <returns>A collection of hooks</returns>
        public async Task<ICollection<MimirorgHookCm>> GetAllHooksForCache(CacheKey key)
        {
            var hooks = _mimirorgHookRepository.GetAll().Where(x => x.Key == key).Include(x => x.Company).Select(x => x.ToContentModel()).ToList();
            foreach (var mimirorgHookCm in hooks)
            {
                if (mimirorgHookCm.Company == null)
                    continue;

                mimirorgHookCm.Company.Logo = $"{_applicationSettings.ApplicationUrl}/logo/{mimirorgHookCm.Company.Id}.svg";
            }
            return await Task.FromResult(hooks);
        }

        /// <summary>
        /// Create a new hook
        /// </summary>
        /// <param name="hook">The hook to be created</param>
        /// <returns>The created hook</returns>
        public async Task<MimirorgHookCm> CreateHook(MimirorgHookAm hook)
        {
            var validation = hook.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register hook: {hook?.CompanyId}-{hook?.Key}", validation);

            var existingHook = await _mimirorgHookRepository.FindBy(x => x.CompanyId == hook.CompanyId && x.Key == hook.Key && x.Iri == hook.Iri).FirstOrDefaultAsync();
            if (existingHook != null)
                throw new MimirorgBadRequestException("The hook already exist");

            var hookDm = hook.ToDomainModel();
            await _mimirorgHookRepository.CreateAsync(hookDm);
            await _mimirorgHookRepository.SaveAsync();
            var hookCm = hookDm.ToContentModel();
            if (hookCm.Company != null)
            {
                hookCm.Company.Logo = $"{_applicationSettings.ApplicationUrl}/logo/{hookCm.Company.Id}.svg";
            }

            return hookCm;
        }
    }
}