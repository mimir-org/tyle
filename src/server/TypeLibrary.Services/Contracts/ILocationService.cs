using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationLibAm>> GetLocations();
        Task<LocationLibAm> UpdateLocation(LocationLibAm dataAm);
        Task<LocationLibAm> CreateLocation(LocationLibAm dataAm);
        Task CreateLocations(List<LocationLibAm> dataAm);
    }
}