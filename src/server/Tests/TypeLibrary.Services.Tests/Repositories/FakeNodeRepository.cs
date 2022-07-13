using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public class FakeNodeRepository : INodeRepository
    {
        //public FakeNodeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        //{
        //    // Add some test data
        //    var a = new NodeLibDm
        //    {
        //        Id = "Fake_Node_A",
        //        FirstVersionId = "Fake_Node_A",
        //        Name = "Pump",
        //        RdsCode = "Fake_Rds",
        //        PurposeName = "Fake_Purpose",
        //        Aspect = Aspect.Function,
        //        Version = "1.0",
        //        Created = DateTime.Now,
        //        CreatedBy = "Test Tester"
        //    };

        //    Context.Add(a);
        //    Context.SaveChanges();
        //    Detach(a);
        //}

        //public IQueryable<NodeLibDm> GetAllNodes()
        //{
        //    return GetAll()
        //        .Include(x => x.Parent)
        //        .Include(x => x.NodeTerminals)
        //        .Include(x => x.Attributes)
        //        .Include(x => x.Simples);
        //}

        //public IQueryable<NodeLibDm> FindNode(string id)
        //{
        //    return FindBy(x => x.Id == id)
        //        .Include(x => x.Parent)
        //        .Include(x => x.NodeTerminals)
        //        .Include(x => x.Attributes)
        //        .Include(x => x.Simples);
        //}

        public IEnumerable<NodeLibDm> Get()
        {
            throw new NotImplementedException();
        }

        public Task<NodeLibDm> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<NodeLibDm> Create(NodeLibDm node)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void ClearAllChangeTrackers()
        {
            throw new NotImplementedException();
        }
    }
}