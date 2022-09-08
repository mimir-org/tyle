using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Factories
{
    public interface ITransportFactory
    {
        Task<TransportLibDm> Get(string id);
    }
}