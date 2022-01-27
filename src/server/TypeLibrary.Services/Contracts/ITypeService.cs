using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Client;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface ITypeService
    {
        Task<TypeDm> GetTypeById(string id, bool ignoreNotFound = false);
        IEnumerable<TypeAm> GetAllTypes();
        Task<IEnumerable<TypeDm>> CreateTypes(ICollection<TypeAm> createTypes);
        Task<T> CreateType<T>(TypeAm typeAm) where T : class, new();
        Task<T> UpdateType<T>(string id, TypeAm typeAm, bool updateMajorVersion, bool updateMinorVersion) where T : class, new();
        Task DeleteType(string id);
        Task<TypeAm> ConvertToCreateType(string id, LibraryFilter @enum);
        Task<SimpleDm> CreateSimpleType(SimpleAm simple);
        Task CreateSimpleTypes(ICollection<SimpleAm> simpleTypes);
        IEnumerable<SimpleDm> GetSimpleTypes();
        void ClearAllChangeTracker();

        Task<TypeCm> GetType(string searchString);
        Task<IEnumerable<NodeCm>> GetNodes();
        Task<IEnumerable<TransportCm>> GetTransports();
        Task<IEnumerable<InterfaceCm>> GetInterfaces();
        Task<IEnumerable<SubProjectCm>> GetSubProjects(string searchString = null);

    }
}