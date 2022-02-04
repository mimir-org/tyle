using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface ITypeService
    {
        Task<TypeLibDm> GetTypeById(string id, bool ignoreNotFound = false);
        IEnumerable<TypeLibAm> GetAllTypes();
        Task<IEnumerable<TypeLibDm>> CreateTypes(ICollection<TypeLibAm> typeAmList);
        Task<T> CreateType<T>(TypeLibAm typeAm) where T : class, new();
        Task<T> UpdateType<T>(string id, TypeLibAm typeAm, bool updateMajorVersion, bool updateMinorVersion) where T : class, new();
        Task DeleteType(string id);
        Task<TypeLibAm> ConvertToCreateType(string id, LibraryFilter @enum);
        Task<SimpleLibDm> CreateSimpleType(SimpleLibAm simpleAm);
        Task CreateSimpleTypes(ICollection<SimpleLibAm> simpleAmList);
        IEnumerable<SimpleLibDm> GetSimpleTypes();
        void ClearAllChangeTracker();

        Task<TypeLibCm> GetType(string searchString);
        Task<IEnumerable<NodeLibCm>> GetNodes();
        Task<IEnumerable<TransportLibCm>> GetTransports();
        Task<IEnumerable<InterfaceLibCm>> GetInterfaces();
        Task<IEnumerable<SubProjectLibCm>> GetSubProjects(string searchString = null);

    }
}