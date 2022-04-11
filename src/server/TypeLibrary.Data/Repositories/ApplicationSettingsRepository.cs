using System.Collections.Generic;
using System.Linq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class ApplicationSettingsRepository : IApplicationSettingsRepository
    {
        private ICollection<OntologyIriLibDm> _uris;

        public ApplicationSettingsRepository()
        {
            InitialData();
        }

        public string GetCurrentOntologyIri()
        {
            return _uris.FirstOrDefault(x => x.CurrentVersion)?.Iri;
        }

        private void InitialData()
        {
            _uris = new List<OntologyIriLibDm>
            {
                new()
                {
                    Id = 1,
                    CurrentVersion = true,
                    Year = 2022,
                    Iri = @"https://typelibrary.com/1/2022/"
                }
            };
        }
    }
}
