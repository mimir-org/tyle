using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfAttributeGroupAttributeRepository : IGenericRepository<TypeLibraryDbContext, AttributeGroupAttributesLibDm>
    {
    }
}