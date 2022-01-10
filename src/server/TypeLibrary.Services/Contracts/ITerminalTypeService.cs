using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application.TypeEditor;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalTypeService
    {
        IEnumerable<TerminalType> GetTerminals();
        Dictionary<string, List<TerminalType>> GetTerminalsByCategory();
        Task<TerminalType> CreateTerminalType(CreateTerminalType createTerminalType);
        Task<List<TerminalType>> CreateTerminalTypes(List<CreateTerminalType> createTerminalTypes);
    }
}
