using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeAspectRepository : GenericRepository<TypeLibraryDbContext, AttributeAspectLibDm>, IEfAttributeAspectRepository
    {
        public EfAttributeAspectRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get all aspect attributes
        /// </summary>
        /// <returns>A collection of aspect attributes</returns>
        /// <remarks>Only aspect attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributeAspectLibDm> GetAspects()
        {
            return GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a new aspect attribute
        /// </summary>
        /// <param name="aspect">The aspect attribute that should be created</param>
        /// <returns>An aspect attribute</returns>
        public async Task<AttributeAspectLibDm> CreateAspect(AttributeAspectLibDm aspect)
        {
            await CreateAsync(aspect);
            await SaveAsync();
            return aspect;
        }
    }
}