using System.Collections.Generic;
using System.Linq;
using Mimirorg.Authentication.Contracts;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Core.Factories
{
    public class CompanyFactory : ICompanyFactory
    {
        private ICollection<MimirorgCompanyCm> _companies;
        private readonly IMimirorgCompanyService _mimirorgCompanyService;

        public CompanyFactory(IMimirorgCompanyService mimirorgCompanyService)
        {
            _mimirorgCompanyService = mimirorgCompanyService;
        }

        public string GetCompanyName(int companyId)
        {
            _companies ??= _mimirorgCompanyService.GetAllCompanies().Result;

            var company = _companies.FirstOrDefault(x => x.Id == companyId);
            return company?.Name;
        }
    }
}