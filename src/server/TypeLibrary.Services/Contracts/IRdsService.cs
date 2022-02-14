using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Contracts
{
    public interface IRdsService
    {
        IEnumerable<RdsLibDm> GetRds(Aspect aspect);
        IEnumerable<RdsLibDm> GetRds();
        Task<RdsLibDm> CreateRds(RdsLibAm rdsAm);
        Task<List<RdsLibDm>> CreateRdsAsync(List<RdsLibAm> createRds);
        Task<IEnumerable<RdsCategoryLibAm>> GetRdsCategories();
        Task<RdsCategoryLibAm> UpdateRdsCategory(RdsCategoryLibAm dataAm);
        Task<RdsCategoryLibAm> CreateRdsCategory(RdsCategoryLibAm dataAm);
        Task CreateRdsCategories(List<RdsCategoryLibAm> dataAm);
    }
}
