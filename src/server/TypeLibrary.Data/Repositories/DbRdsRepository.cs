using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbRdsRepository : IRdsRepository
    {
        private readonly IEfRdsRepository _rdsRepository;

        public DbRdsRepository(IEfRdsRepository rdsRepository)
        {
            _rdsRepository = rdsRepository;
        }

        public IEnumerable<RdsLibDm> Get()
        {
            return _rdsRepository.GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(List<RdsLibDm> dataDm)
        {
            foreach (var data in dataDm)
                _rdsRepository.Attach(data, EntityState.Added);

            await _rdsRepository.SaveAsync();

            foreach (var data in dataDm)
                _rdsRepository.Detach(data);
        }
    }
}