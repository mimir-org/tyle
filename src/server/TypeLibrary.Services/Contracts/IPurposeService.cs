using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IPurposeService
    {
        Task<IEnumerable<PurposeAm>> GetPurposes();
        Task<PurposeAm> UpdatePurpose(PurposeAm dataAm);
        Task<PurposeAm> CreatePurpose(PurposeAm dataAm);
        Task CreatePurposes(List<PurposeAm> dataAm);
    }
}