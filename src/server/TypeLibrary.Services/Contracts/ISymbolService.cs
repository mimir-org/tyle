using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface ISymbolService
{
    IEnumerable<SymbolLibCm> Get();
    SymbolLibCm Get(string id);
    Task Create(IEnumerable<SymbolLibAm> symbolDataList, string createdBy = null);
}