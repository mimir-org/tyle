using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitAm>> GetUnits();
        Task<UnitAm> UpdateUnit(UnitAm dataAm);
        Task<UnitAm> CreateUnit(UnitAm dataAm);
        Task CreateUnits(List<UnitAm> dataAm);
    }
}