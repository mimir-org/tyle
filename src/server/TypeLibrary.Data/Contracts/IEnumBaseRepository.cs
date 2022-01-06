using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.Enums;

namespace TypeLibrary.Data.Contracts
{
    public interface IEnumBaseRepository : IGenericRepository<ModelBuilderDbContext, EnumBase>
    {
        
    }
}
