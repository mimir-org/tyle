using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbUnitRepository : IUnitRepository
    {
        private readonly IEfUnitRepository _unitRepository;

        public DbUnitRepository(IEfUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public IEnumerable<UnitLibDm> Get()
        {
            return _unitRepository.GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(List<UnitLibDm> dataDm)
        {
            foreach (var data in dataDm)
                _unitRepository.Attach(data, EntityState.Added);

            await _unitRepository.SaveAsync();

            foreach (var data in dataDm)
                _unitRepository.Detach(data);
        }

        public void ClearAllChangeTrackers()
        {
            _unitRepository?.Context?.ChangeTracker.Clear();
        }
    }
}