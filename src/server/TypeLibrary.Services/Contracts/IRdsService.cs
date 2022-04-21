using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IRdsService
    {
        IEnumerable<RdsLibCm> GetRds();
        Task<RdsLibCm> CreateRds(RdsLibAm rdsAm);
        Task<List<RdsLibCm>> CreateRdsAsync(List<RdsLibAm> createRds);
    }
}
