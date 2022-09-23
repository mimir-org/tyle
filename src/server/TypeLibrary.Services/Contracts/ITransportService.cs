using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ITransportService
    {
        Task<TransportLibCm> Get(string id);
        Task<IEnumerable<TransportLibCm>> GetLatestVersions();
        Task<TransportLibCm> Create(TransportLibAm dataAm, bool resetVersion);
        Task<IEnumerable<TransportLibCm>> Create(IEnumerable<TransportLibAm> dataAms, bool createdBySystem = false);
        Task<TransportLibCm> Update(TransportLibAm dataAm, string id);
        Task<TransportLibCm> UpdateState(string id, State state);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string transportId, int companyId);
    }
}