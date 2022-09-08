using System.Threading.Tasks;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Factories;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Factories
{
    public class TransportFactory : ITransportFactory
    {
        private readonly ITransportRepository _transportRepository;

        public TransportFactory(ITransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        public async Task<TransportLibDm> Get(string id)
        {
            var transport = await _transportRepository.Get(id);
            return transport;
        }
    }
}