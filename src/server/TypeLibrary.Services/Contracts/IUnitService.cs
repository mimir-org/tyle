using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitLibCm>> GetUnits();
        Task<UnitLibCm> UpdateUnit(UnitLibAm dataAm, string id);
        Task<UnitLibCm> CreateUnit(UnitLibAm dataAm);
        Task CreateUnits(List<UnitLibAm> dataAm, bool createdBySystem = false);
    }
}