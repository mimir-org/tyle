using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbAttributeRepository : IAttributeRepository
    {
        private readonly IEfAttributeRepository _efAttributeRepository;
        private readonly IEfUnitRepository _efUnitRepository;
        private readonly IEfAttributePredefinedRepository _attributePredefinedRepository;

        public DbAttributeRepository(IEfAttributeRepository efAttributeRepository, IEfUnitRepository efUnitRepository, IEfAttributePredefinedRepository attributePredefinedRepository)
        {
            _efAttributeRepository = efAttributeRepository;
            _efUnitRepository = efUnitRepository;
            _attributePredefinedRepository = attributePredefinedRepository;
        }

        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns>A collection of attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributeLibDm> GetAllAttributes()
        {
            return _efAttributeRepository.GetAll()
                .Where(x => !x.Deleted)
                .Include(x => x.Units);
        }

        /// <summary>
        /// Get attribute by id
        /// </summary>
        /// <param name="id">The id of the attribute</param>
        /// <returns>An attribute</returns>
        public async Task<AttributeLibDm> GetAttribute(string id)
        {
            return await _efAttributeRepository.FindBy(x => x.Id == id)
                .Include(x => x.Units)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        public async Task<AttributeLibDm> CreateAttribute(AttributeLibDm attribute)
        {
            _efUnitRepository.Attach(attribute.Units, EntityState.Unchanged);
            await _efAttributeRepository.CreateAsync(attribute);
            await _efAttributeRepository.SaveAsync();
            _efUnitRepository.Detach(attribute.Units);
            _efAttributeRepository.Detach(attribute);
            return attribute;
        }

        /// <summary>
        /// Get all predefined attributes
        /// </summary>
        /// <returns>A collection of predefined attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributePredefinedLibDm> GetAllAttributePredefine()
        {
            return _attributePredefinedRepository.GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a predefined attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        public async Task<AttributePredefinedLibDm> CreateAttributePredefined(AttributePredefinedLibDm attribute)
        {
            await _attributePredefinedRepository.CreateAsync(attribute);
            await _attributePredefinedRepository.SaveAsync();
            return attribute;
        }
    }
}
