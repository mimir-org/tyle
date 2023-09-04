using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfAttributeGroupRepository : IGenericRepository<TypeLibraryDbContext, AttributeGroupLibDm>, IAttributeRepository
    {

    }
}
