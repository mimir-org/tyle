using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributePredefinedRepository : GenericRepository<TypeLibraryDbContext, AttributePredefinedLibDm>, IEfAttributePredefinedRepository
    {
        public EfAttributePredefinedRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get all predefined attributes
        /// </summary>
        /// <returns>A collection of predefined attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributePredefinedLibDm> GetPredefined()
        {
            return GetAll();
        }

        /// <summary>
        /// Create a predefined attribute
        /// </summary>
        /// <param name="predefined">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        public async Task<AttributePredefinedLibDm> CreatePredefined(AttributePredefinedLibDm predefined)
        {
            await CreateAsync(predefined);
            await SaveAsync();
            return predefined;
        }
    }
}