using System.Collections.Generic;
using System.Threading.Tasks;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data.TypeEditor;

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
