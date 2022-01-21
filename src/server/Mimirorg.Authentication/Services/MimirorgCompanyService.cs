using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Content;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;

namespace Mimirorg.Authentication.Services
{
    public class MimirorgCompanyService : IMimirorgCompanyService
    {
        private readonly IMimirorgCompanyRepository _mimirorgCompanyRepository;

        public MimirorgCompanyService(IMimirorgCompanyRepository mimirorgCompanyRepository)
        {
            _mimirorgCompanyRepository = mimirorgCompanyRepository;
        }

        public async Task<MimirorgCompanyCm> CreateCompany(MimirorgCompanyAm company)
        {
            var validation = company.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register: {company.DisplayName ?? company.Name}", validation);

            if (_mimirorgCompanyRepository.FindBy(x => x.Name.Equals(company.Name, StringComparison.InvariantCultureIgnoreCase)).Any())
                throw new MimirorgBadRequestException($"{nameof(company.Name)} must be unique", new Validation(nameof(company.Name), $"{nameof(company.Name)} must be unique"));
                

            var domainCompany = company.ToDomainModel();
            await _mimirorgCompanyRepository.CreateAsync(domainCompany);
            await _mimirorgCompanyRepository.Context.SaveChangesAsync();

            if (domainCompany.Id < 1)
                throw new MimirorgInvalidOperationException($"Could not create company with name {company.Name}");

            return domainCompany.ToContentModel();
        }

        public async Task<IEnumerable<MimirorgCompanyCm>> GetAllCompanies()
        {
            var companies = _mimirorgCompanyRepository.GetAll().Select(x => x.ToContentModel()).ToList();
            return await Task.FromResult(companies);
        }

        public async Task<MimirorgCompanyCm> GetCompanyById(int id)
        {
            var company = await _mimirorgCompanyRepository.FindBy(x => x.Id == id).Include(x => x.Manager).FirstOrDefaultAsync();
            if (company == null)
                throw new MimirorgNotFoundException($"Could not find company with id {id}");

            return company.ToContentModel();
        }

        public async Task<MimirorgCompanyCm> UpdateCompany(int id, MimirorgCompanyAm company)
        {
            var validation = company.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register: {company.DisplayName ?? company.Name}", validation);

            var exist = await _mimirorgCompanyRepository.GetAll().AnyAsync(x => x.Id == id);
            if(!exist)
                throw new MimirorgNotFoundException($"Could not find company with id {id}");

            var domainCompany = company.ToDomainModel();
            domainCompany.Id = id;

            _mimirorgCompanyRepository.Update(domainCompany);
            await _mimirorgCompanyRepository.SaveAsync();
            return domainCompany.ToContentModel();
        }

        public async Task<bool> DeleteCompany(int id)
        {
            var exist = await _mimirorgCompanyRepository.GetAll().AnyAsync(x => x.Id == id);
            if (!exist)
                throw new MimirorgNotFoundException($"Could not find company with id {id}");

            await _mimirorgCompanyRepository.Delete(id);
            var status = await _mimirorgCompanyRepository.Context.SaveChangesAsync();
            return status == 1;
        }
    }
}
