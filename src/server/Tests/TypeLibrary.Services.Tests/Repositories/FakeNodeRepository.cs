using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public sealed class FakeNodeRepository : GenericRepository<TypeLibraryDbContext, NodeLibDm>, INodeRepository
    {
        public FakeNodeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
            // Add some test data
            var a = new NodeLibDm
            {
                Id = "Fake_Node_A",
                FirstVersionId = "Fake_Node_A",
                Name = "Pump",
                RdsId = "Fake_Rds",
                PurposeId = "Fake_Purpose",
                Aspect = Aspect.Function,
                Version = "1.0",
                Created = DateTime.Now,
                CreatedBy = "Test Tester"
            };

            Context.Add(a);
            Context.SaveChanges();
            Detach(a);
        }

        public IQueryable<NodeLibDm> GetAllNodes()
        {
            return GetAll()
                .Include(x => x.Rds)
                .Include(x => x.Purpose)
                .Include(x => x.Parent)
                .Include(x => x.Blob)
                .Include(x => x.AttributeAspect)
                .Include(x => x.NodeTerminals)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }

        public IQueryable<NodeLibDm> FindNode(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.Rds)
                .Include(x => x.Purpose)
                .Include(x => x.Parent)
                .Include(x => x.Blob)
                .Include(x => x.AttributeAspect)
                .Include(x => x.NodeTerminals)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }
    }
}
