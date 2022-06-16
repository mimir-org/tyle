using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ISimpleService
    {
        Task<SimpleLibCm> Get(string id);
        Task<IEnumerable<SimpleLibCm>> Get();
        Task<IEnumerable<SimpleLibCm>> Create(IEnumerable<SimpleLibAm> simpleAms, bool createdBySystem = false);
    }
}