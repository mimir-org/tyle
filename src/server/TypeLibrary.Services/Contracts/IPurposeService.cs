using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IPurposeService
    {
        Task<IEnumerable<PurposeLibAm>> GetPurposes();
        Task<PurposeLibAm> UpdatePurpose(PurposeLibAm dataAm);
        Task<PurposeLibAm> CreatePurpose(PurposeLibAm dataAm);
        Task CreatePurposes(List<PurposeLibAm> dataAm);
    }
}