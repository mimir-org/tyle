using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalTypeService
    {
        IEnumerable<TerminalType> GetTerminals();
        List<TerminalType> GetTerminalsByCategory();
        Task<TerminalType> CreateTerminalType(CreateTerminalType createTerminalType);
        Task<List<TerminalType>> CreateTerminalTypes(List<CreateTerminalType> createTerminalTypes);
    }
}
