using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitLibCm>> Get();
        Task Create(List<UnitLibAm> dataAm, bool createdBySystem = false);
    }
}