using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeTypeRepository : IGenericRepository<ModelBuilderDbContext, AttributeType>
    {
    }
}
