using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationAm>> GetLocations();
        Task<LocationAm> UpdateLocation(LocationAm dataAm);
        Task<LocationAm> CreateLocation(LocationAm dataAm);
        Task CreateLocations(List<LocationAm> dataAm);
    }
}