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

        public async Task ChangeState(string id, State state, bool sendStateEmail)
        {
            var attribute = await GetAsync(id);
            await SaveAsync();
            Detach(attribute);
        }


        public async Task<AttributeGroupLibDm> Create(AttributeGroupLibDm attributeGroupLibDm)
        {
            //BUG: TODO if list is null it fails

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
            {

                return FindBy(x => x.Id == id)
                    .Include(x => x.AttributeGroupAttributes)
                    .ThenInclude(x => x.Attribute)
                    .FirstOrDefault();
            }
        }

        public AttributeGroupLibDm Update(string id, AttributeGroupLibDm attributeGroupLibDm)
        {
            var item = GetSingleAttributeGroup(id);
            //Do the updates
            Update(item);
            return item;
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }
    }
}