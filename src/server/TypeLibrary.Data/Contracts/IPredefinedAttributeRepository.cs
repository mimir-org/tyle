using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data;

namespace TypeLibrary.Data.Contracts
{
    public interface IPredefinedAttributeRepository : IGenericRepository<ModelBuilderDbContext, PredefinedAttribute>
    {
    }
}
