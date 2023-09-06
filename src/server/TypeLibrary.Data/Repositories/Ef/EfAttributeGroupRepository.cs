using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Util;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
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
            attribute.State = state;
            await SaveAsync();
            Detach(attribute);
        }


        public async Task<AttributeGroupLibDm> Create(AttributeGroupLibDm attributeGroupLibDm, List<string> attributesInGroup, string createdBy = null)
        {            
            foreach (var item in attributesInGroup)            
            {
                //attributeGroupLibDm.Att = item;
                await CreateAsync(attributeGroupLibDm);
            }
            await SaveAsync();
            Detach(attributeGroupLibDm);



            return attributeGroupLibDm;
        }

        public IEnumerable<AttributeGroupLibDm> GetAttributeGroupList(string searchText = null)
        {
            return GetAll().AsSplitQuery();
        }

        public AttributeGroupLibDm GetSingleAttributeGroup(string id)
        {
            {
                return FindBy(x => x.Id == id)                                       
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
