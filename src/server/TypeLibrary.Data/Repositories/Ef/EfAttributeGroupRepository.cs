using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{

    public class EfAttributeGroupRepository : GenericRepository<TypeLibraryDbContext, AttributeGroupLibDm>, IEfAttributeGroupRepository
    {
        public EfAttributeGroupRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AttributeGroupLibDm> Create(AttributeGroupLibDm attributeGroupLibDm)
        {

            await CreateAsync(attributeGroupLibDm);
            await SaveAsync();
            Detach(attributeGroupLibDm);

            return attributeGroupLibDm;



        }

        public IEnumerable<AttributeGroupLibDm> GetAttributeGroupList()
        {
            return GetAll().Include(x => x.AttributeGroupAttributes)
                    .ThenInclude(x => x.Attribute).AsSplitQuery();
        }

        public AttributeGroupLibDm GetSingleAttributeGroup(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.AttributeGroupAttributes)
                .ThenInclude(x => x.Attribute)
                .FirstOrDefault();
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }
    }
}