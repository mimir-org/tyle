using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public sealed class FakeNodeRepository : GenericRepository<TypeLibraryDbContext, NodeLibDm>, IEfNodeRepository
    {
        public FakeNodeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
            // Add some test data
            var a = new NodeLibDm
            {
                Id = "Fake_Node_A",
                FirstVersionId = "Fake_Node_A",
                Name = "Pump",
                RdsCode = "Fake_Rds",
                PurposeName = "Fake_Purpose",
                Aspect = Aspect.Function,
                Version = "1.0",
                Created = DateTime.Now,
                CreatedBy = "Test Tester"
            };

            Context.Add(a);
            Context.SaveChanges();
            Detach(a);
        }

        public async Task<NodeLibDm> Get(string id)
        {
            return await GetAsync(id);
        }

        public IQueryable<NodeLibDm> GetAllNodes()
        {
            return GetAll()
                .Include(x => x.Parent)
                .Include(x => x.NodeTerminals)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }

        public IQueryable<NodeLibDm> FindNode(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.Parent)
                .Include(x => x.NodeTerminals)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }
    }
}