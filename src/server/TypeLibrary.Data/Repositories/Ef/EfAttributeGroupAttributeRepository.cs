using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeGroupAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeGroupAttributesLibDm>, IEfAttributeGroupAttributeRepository
    {
        public EfAttributeGroupAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}