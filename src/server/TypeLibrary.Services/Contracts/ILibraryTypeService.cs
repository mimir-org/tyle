using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Contracts
{
    public interface ILibraryTypeService
    {
        Task<LibraryTypeLibDm> GetLibraryTypeById(string id, bool ignoreNotFound = false);
        Task<LibraryTypeLibCm> GetLibraryType(string searchString);
        IEnumerable<LibraryTypeLibAm> GetAllLibraryTypes();
        Task<IEnumerable<LibraryTypeLibDm>> CreateLibraryTypes(ICollection<LibraryTypeLibAm> typeAmList);
        Task<T> CreateLibraryType<T>(LibraryTypeLibAm libraryTypeAm) where T : class, new();
        Task<T> UpdateLibraryType<T>(string id, LibraryTypeLibAm libraryTypeAm, bool updateMajorVersion, bool updateMinorVersion) where T : class, new();
        Task DeleteLibraryType(string id);
        Task<LibraryTypeLibAm> ConvertToCreateLibraryType(string id, LibraryTypeFilter @enum);

        Task<SimpleLibDm> CreateSimpleType(SimpleLibAm simpleAm);
        Task CreateSimpleTypes(ICollection<SimpleLibAm> simpleAmList);
        IEnumerable<SimpleLibDm> GetSimpleTypes();
        
        Task<IEnumerable<NodeLibCm>> GetNodes();
        Task<IEnumerable<TransportLibCm>> GetTransports();
        Task<IEnumerable<InterfaceLibCm>> GetInterfaces();
        
        void ClearAllChangeTracker();
    }
}