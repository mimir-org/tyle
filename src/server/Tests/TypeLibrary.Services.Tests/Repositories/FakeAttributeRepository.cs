using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public sealed class FakeAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeLibDm>, IEfAttributeRepository
    {
        public FakeAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
            // Add some test data
            var a = new AttributeLibDm
            {
                Id = "Fake_Attribute_A",
                Name = "Fake_Pressure",
                Aspect = Aspect.Function,
                Discipline = Discipline.Process,
                Select = Select.None,
                AttributeQualifier = "Capacity",
                AttributeSource = "Required",
                AttributeCondition = "Minimum",
                AttributeFormat = "Float"
            };

            Context.Add(a);
            Context.SaveChanges();
            Detach(a);
        }
    }
}