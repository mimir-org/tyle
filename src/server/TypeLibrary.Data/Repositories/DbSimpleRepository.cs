using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbSimpleRepository : ISimpleRepository
    {
        private readonly IEfSimpleRepository _simpleRepository;
        private readonly IEfAttributeRepository _attributeRepository;

        public DbSimpleRepository(IEfSimpleRepository simpleRepository, IEfAttributeRepository attributeRepository)
        {
            _simpleRepository = simpleRepository;
            _attributeRepository = attributeRepository;
        }

        public async Task<SimpleLibDm> Get(string id)
        {
            return await _simpleRepository.FindBy(x => x.Id == id && !x.Deleted).FirstAsync();
        }

        public IEnumerable<SimpleLibDm> Get()
        {
            return _simpleRepository.GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(SimpleLibDm dataDm)
        {
            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.Attach(dataDm.Attributes, EntityState.Unchanged);

            await _simpleRepository.CreateAsync(dataDm);
            await _simpleRepository.SaveAsync();

            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.Detach(dataDm.Attributes);

            _simpleRepository.Detach(dataDm);
        }

        public void ClearAllChangeTrackers()
        {
            _simpleRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
        }
    }
}