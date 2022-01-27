using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface IRdsService
    {
        IEnumerable<RdsDm> GetRds(Aspect aspect);
        Task<RdsDm> CreateRds(RdsAm rdsAm);
        Task<List<RdsDm>> CreateRdsAsync(List<RdsAm> createRds);
    }
}
