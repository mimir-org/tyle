using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IRdsService
    {
        IEnumerable<RdsLibCm> GetRds(Aspect aspect);
        IEnumerable<RdsLibCm> GetRds();
        Task<RdsLibCm> CreateRds(RdsLibAm rdsAm);
        Task<List<RdsLibCm>> CreateRdsAsync(List<RdsLibAm> createRds);
        Task<IEnumerable<RdsCategoryLibCm>> GetRdsCategories();
        Task<RdsCategoryLibCm> UpdateRdsCategory(RdsCategoryLibAm dataAm, string id);
        Task<RdsCategoryLibCm> CreateRdsCategory(RdsCategoryLibAm dataAm);
        Task CreateRdsCategories(List<RdsCategoryLibAm> dataAm);
    }
}
