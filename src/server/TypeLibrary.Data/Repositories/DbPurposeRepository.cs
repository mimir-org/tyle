using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbPurposeRepository : IPurposeRepository
    {
        private readonly IEfPurposeRepository _purposeRepository;

        public DbPurposeRepository(IEfPurposeRepository purposeRepository)
        {
            _purposeRepository = purposeRepository;
        }


        public IEnumerable<PurposeLibDm> Get()
        {
            return _purposeRepository.GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(List<PurposeLibDm> dataDm)
        {
            foreach (var data in dataDm)
                _purposeRepository.Attach(data, EntityState.Added);

            await _purposeRepository.SaveAsync();

            foreach (var data in dataDm)
                _purposeRepository.Detach(data);
        }
    }
}