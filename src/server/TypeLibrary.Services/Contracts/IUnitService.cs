using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitLibAm>> GetUnits();
        Task<UnitLibAm> UpdateUnit(UnitLibAm dataAm);
        Task<UnitLibAm> CreateUnit(UnitLibAm dataAm);
        Task CreateUnits(List<UnitLibAm> dataAm);
    }
}