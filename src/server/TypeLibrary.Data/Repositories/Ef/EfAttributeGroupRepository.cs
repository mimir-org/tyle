using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{

    public class EfAttributeGroupRepository : GenericRepository<TypeLibraryDbContext, AttributeGroupLibDm>, IEfAttributeGroupRepository
    {
        public EfAttributeGroupRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
        {
            throw new NotImplementedException();
        }


        public Task<AttributeGroupCm> Create(AttributeGroupAm attributeAm, string createdBy = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AttributeGroupCm>> GetAttributeGroupList(string searchText = null)
        {
            throw new NotImplementedException();
        }

        public async Task<AttributeGroupCm> GetSingleAttributeGroup(string id)
        {
            //TODO
            throw new NotImplementedException();
        }

        public Task<AttributeGroupCm> Update(string id, AttributeGroupAm attributeAm)
        {
            throw new NotImplementedException();
        }
    }
}
