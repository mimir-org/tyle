using System.Collections.Generic;
using System.Linq;
using Mimirorg.Authentication.Contracts;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Core.Factories
{
    public class CompanyFactory : ICompanyFactory
    {
        private readonly ICollection<MimirorgCompanyCm> _companies;

        public CompanyFactory(IMimirorgCompanyService mimirorgCompanyService)
        {
            _companies = mimirorgCompanyService.GetAllCompanies().Result;
        }

        public string GetCompanyName(int companyId)
        {
            var company = _companies.FirstOrDefault(x => x.Id == companyId);
            return company?.Name;
        }
    }
}